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
        Debug.Log($"[StaticEnemy] ���� {amount} �� ���� HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMove>(out var player))
        {
            if (player.IsDashing() && player.IsAtMaxDashSpeed())
            {
                TakeDamage(1);
            }
            else if (player.IsInvincible())
            {
                // ���� �������� ��ð� �ƴϹǷ� �ƹ� �ϵ� ���� ����
                Debug.Log("[StaticEnemy] �÷��̾� ���� ���� - ���� ����");
            }
            else if (collision.gameObject.TryGetComponent<PlayerHealth>(out var health))
            {
                health.TakeDamage(attack);
                Debug.Log($"[StaticEnemy] �÷��̾�� {attack} ����");
            }
        }
    }

    private void Break()
    {
        Debug.Log("[StaticEnemy] �ı���");
        Destroy(gameObject);
    }
}
