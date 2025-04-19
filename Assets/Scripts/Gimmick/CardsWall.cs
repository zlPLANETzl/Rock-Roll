using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsWall : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private float maxHP = 1f;
    private float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
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

    public void OnPlayerDashEnter(PlayerMove player)
    {
        if (player.GetDashSpeed() >= player.GetDashMaxSpeed())
        {
            TakeDamage(1f);
        }
    }
}
