using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 720f;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject rotatingChild; // �ڽ� ȸ���� (ī�� ��)
    [SerializeField] private GameObject hitEffectPrefab;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // �θ� �̵�
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // �ڽ� ȸ�� (�ڱ� ���� Z��)
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
                    Debug.Log("[BossProjectile] �÷��̾� ����/��� ���� - ���� ����, ��� ����");
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
