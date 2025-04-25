using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f; // 수명 설정
    private Vector3 moveDir;

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime); // 일정 시간 뒤 자동 제거
    }

    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerMove>(out var player))
            {
                if (player.IsDashing() && player.IsAtMaxDashSpeed())
                {
                    return; // 대시 중엔 무적
                }
                else if (player.IsInvincible())
                {
                    return; // 무적 중에도 피해 없음
                }
                else
                {
                    PlayerHealth.Instance.TakeDamage(1);
                }
            }

            Destroy(gameObject);
        }
    }
}
