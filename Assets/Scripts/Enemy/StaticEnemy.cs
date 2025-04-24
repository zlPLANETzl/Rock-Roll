using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : EnemyController, IDamageable
{
    void Start()
    {
        InitializeFromTable();
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[StaticEnemy] ���� {amount} �� ���� HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
                    Debug.Log($"[StaticEnemy] �÷��̾�� {attack} ����");
                }
            }
        }
    }

    private void Break()
    {
        Debug.Log("[StaticEnemy] �ı���");
        Destroy(gameObject);
    }
}
