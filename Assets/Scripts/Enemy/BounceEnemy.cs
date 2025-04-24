using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEnemy : EnemyController, IDamageable
{
    private Vector3 moveDirection = Vector3.forward;

    void Start()
    {
        InitializeFromTable();
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
            return;
        }

        if (collision.gameObject.TryGetComponent<PlayerMove>(out var player))
        {
            if (player.IsDashing())
            {
                TakeDamage(1);
            }
            else
            {
                if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
                {
                    target.TakeDamage(attack);
                    Debug.Log($"[BounceEnemy] �÷��̾�� {attack} ����");
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[BounceEnemy] ���� {amount} �� ���� HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[BounceEnemy] �ı���");
        Destroy(gameObject);
    }
}
