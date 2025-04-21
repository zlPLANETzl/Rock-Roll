using UnityEngine;

public class BounceEnemy : EnemyController
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 moveDirection = Vector3.forward;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 벽에 부딪히면 방향 반전
        if (!collision.gameObject.CompareTag("Player"))
        {
            moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal);
            return;
        }

        if (collision.gameObject.TryGetComponent<PlayerMove>(out var player))
        {
            if (player.IsDashing())
            {
                TakeDamage(1);
            }
            else
            {
                AttackPlayer(player.gameObject);
            }
        }
    }
}
