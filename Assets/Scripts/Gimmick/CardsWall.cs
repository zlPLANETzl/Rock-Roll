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
        // �ı� ����Ʈ, �Ҹ� �� �߰� ����
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
