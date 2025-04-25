using UnityEngine;

public class BounceEnemy : EnemyController, IDamageable
{
    private Vector3 moveDirection = Vector3.forward;

    void Start()
    {
        InitializeFromTable();

        moveDirection = transform.forward.normalized;
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
            return;
        }

        if (collision.gameObject.TryGetComponent<PlayerMove>(out var player))
        {
            if (player.IsDashing() && player.IsAtMaxDashSpeed())
            {
                TakeDamage(1);
            }
            else if (player.IsInvincible())
            {
                // 무적 상태지만 대시가 아니므로 아무 일도 하지 않음
                Debug.Log("[BounceEnemy] 플레이어 무적 상태 - 피해 없음");
            }
            else
            {
                PlayerHealth.Instance.TakeDamage(attack);                
            }
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[BounceEnemy] 피해 {amount} → 남은 HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[BounceEnemy] 파괴됨");
        Destroy(gameObject);
    }
}
