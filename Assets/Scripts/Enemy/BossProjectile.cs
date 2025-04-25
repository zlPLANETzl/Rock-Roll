using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f; // ���� ����
    private Vector3 moveDir;

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime); // ���� �ð� �� �ڵ� ����
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
                    return; // ��� �߿� ����
                }
                else if (player.IsInvincible())
                {
                    return; // ���� �߿��� ���� ����
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
