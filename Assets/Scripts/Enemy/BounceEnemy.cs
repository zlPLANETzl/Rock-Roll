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
                // ���� �������� ��ð� �ƴϹǷ� �ƹ� �ϵ� ���� ����
                Debug.Log("[BounceEnemy] �÷��̾� ���� ���� - ���� ����");
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
        Debug.Log($"[BounceEnemy] ���� {amount} �� ���� HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[BounceEnemy] �ı���");
        Destroy(gameObject);
    }
}
