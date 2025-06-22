using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 720f;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject rotatingChild; // 자식 회전용 (카드 모델)
    [SerializeField] private GameObject hitEffectPrefab;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // 부모 이동
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // 자식 회전 (자기 기준 Z축)
        if (rotatingChild != null)
        {
            rotatingChild.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<PlayerMove>(out var playerMove))
            {
                if (playerMove.IsInvincible() || playerMove.IsDashing())
                {
                    Debug.Log("[BossProjectile] 플레이어 무적/대시 상태 - 피해 없음, 대시 유지");
                }
                else
                {
                    PlayerHealth.Instance.TakeDamage(damage);
                }
            }
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
