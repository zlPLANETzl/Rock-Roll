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
        Debug.Log($"[StaticEnemy] 피해 {amount} → 남은 HP: {hp}");
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
                    Debug.Log($"[StaticEnemy] 플레이어에게 {attack} 피해");
                }
            }
        }
    }

    private void Break()
    {
        Debug.Log("[StaticEnemy] 파괴됨");
        Destroy(gameObject);
    }
}
