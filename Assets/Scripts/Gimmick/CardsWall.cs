using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsWall : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHP = 1;
    private int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        // 파괴 이펙트, 소리 등 추가 가능
        Destroy(gameObject);
    }
}