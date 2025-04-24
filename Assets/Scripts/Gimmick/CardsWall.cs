using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsWall : EnemyController, IDamageable
{
    void Start()
    {
        InitializeFromTable();
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[CardsWall] ÇÇÇØ {amount} ¡æ ³²Àº HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[CardsWall] ÆÄ±«µÊ");
        Destroy(gameObject);
    }
}
