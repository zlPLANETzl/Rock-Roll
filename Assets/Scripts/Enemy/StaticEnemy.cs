using UnityEngine;

public class StaticEnemy : EnemyController
{
    private void OnCollisionEnter(Collision collision)
    {
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
