using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHp = 1;
    [SerializeField] protected int attackPower = 1;

    protected int currentHp;

    protected virtual void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;
        Debug.Log($"[Enemy] ����: {amount} �� ���� ü��: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void AttackPlayer(GameObject target)
    {
        if (target.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(attackPower);
        }
    }

    protected virtual void Die()
    {
        Debug.Log("[Enemy] ��� ó��");
        Destroy(gameObject);
    }
}
