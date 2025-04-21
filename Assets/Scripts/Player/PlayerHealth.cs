using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private int maxHp = 3;
    
    public int playerCurrentHp;

    private void Awake()
    {
        playerCurrentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        playerCurrentHp -= amount;
        Debug.Log($"[Player] 피해: {amount} → 현재 체력: {playerCurrentHp}");

        if (playerCurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[Player] 사망 처리");
        // 게임오버, 리스폰 등 후속 처리
    }

    public void Heal(int amount)
    {
        playerCurrentHp = Mathf.Min(playerCurrentHp + amount, maxHp);
        Debug.Log($"[Player] 회복: {amount} → 현재 체력: {playerCurrentHp}");
    }

    public int GetHp()
    {
        return playerCurrentHp;
    }
}
