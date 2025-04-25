using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static PlayerHealth Instance { get; private set; }

    [SerializeField] private PlayerInvincibilityEffect invincibilityEffect;
    [SerializeField] private int maxHp = 3;
    public int CurrentHp { get; private set; }
    private bool isInvincible = false;

    [SerializeField] private PlayerHealthUI healthUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        CurrentHp = maxHp;
        healthUI?.UpdateHeartDisplay(CurrentHp);
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        CurrentHp -= amount;
        Debug.Log($"[Player] ����: {amount} �� ���� ü��: {CurrentHp}");

        healthUI?.UpdateHeartDisplay(CurrentHp);

        if (CurrentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(TemporaryInvincibility(1f));
        }
    }

    public void Heal(int amount)
    {
        CurrentHp = Mathf.Min(CurrentHp + amount, maxHp);
        Debug.Log($"[Player] ȸ��: {amount} �� ���� ü��: {CurrentHp}");
        healthUI?.UpdateHeartDisplay(CurrentHp);
    }

    public bool IsInvincible() => isInvincible;

    private IEnumerator TemporaryInvincibility(float duration)
    {
        isInvincible = true;

        Debug.Log("[PlayerHealth] ���� �ڷ�ƾ ���۵�");

        if (invincibilityEffect != null)
        {
            Debug.Log("[PlayerHealth] ������ ȣ�� ����");
            invincibilityEffect.Flash();
        }
        else
        {
            Debug.LogWarning("[PlayerHealth] invincibilityEffect�� ������� ����");
        }

        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("[Player] ��� ó��");
        GameOverUI.Instance.ShowGameOver();  // UI ȣ��
    }

    public int GetHp()
    {
        return CurrentHp;
    }
}
