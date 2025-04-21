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
        Debug.Log($"[Player] ����: {amount} �� ���� ü��: {playerCurrentHp}");

        if (playerCurrentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[Player] ��� ó��");
        // ���ӿ���, ������ �� �ļ� ó��
    }

    public void Heal(int amount)
    {
        playerCurrentHp = Mathf.Min(playerCurrentHp + amount, maxHp);
        Debug.Log($"[Player] ȸ��: {amount} �� ���� ü��: {playerCurrentHp}");
    }

    public int GetHp()
    {
        return playerCurrentHp;
    }
}
