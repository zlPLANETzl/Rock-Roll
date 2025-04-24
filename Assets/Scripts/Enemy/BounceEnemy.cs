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
                    Debug.Log($"[BounceEnemy] 플레이어에게 {attack} 피해");
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[BounceEnemy] 피해 {amount} → 남은 HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[BounceEnemy] 파괴됨");
        Destroy(gameObject);
    }
}
