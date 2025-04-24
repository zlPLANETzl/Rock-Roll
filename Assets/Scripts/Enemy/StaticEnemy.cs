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
        Debug.Log($"[StaticEnemy] 피해 {amount} → 남은 HP: {hp}");
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
                // 무적 상태지만 대시가 아니므로 아무 일도 하지 않음
                Debug.Log("[StaticEnemy] 플레이어 무적 상태 - 피해 없음");
            }
            else if (collision.gameObject.TryGetComponent<PlayerHealth>(out var health))
            {
                health.TakeDamage(attack);
                Debug.Log($"[StaticEnemy] 플레이어에게 {attack} 피해");
            }
        }
    }

    private void Break()
    {
        Debug.Log("[StaticEnemy] 파괴됨");
        Destroy(gameObject);
    }
}
