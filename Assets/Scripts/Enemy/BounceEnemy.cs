using UnityEngine;
using System.Collections;

public class BounceEnemy : EnemyController, IDamageable
{
    private enum BounceState { Move, Turn1, Pause, Turn2 }

    private BounceState currentState = BounceState.Move;
    private Vector3 moveDirection = Vector3.forward;
    private Quaternion targetRotation;

    [SerializeField] private float rotateSpeed = 360f;
    [SerializeField] private float pauseDuration = 0.2f;

    void Start()
    {
        InitializeFromTable();
        moveDirection = transform.forward.normalized;
    }

    void Update()
    {
        switch (currentState)
        {
            case BounceState.Move:
                transform.position += moveDirection * speed * Time.deltaTime;
                break;

            case BounceState.Turn1:
            case BounceState.Turn2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
                {
                    if (currentState == BounceState.Turn1)
                    {
                        StartCoroutine(EnterPauseState());
                    }
                    else if (currentState == BounceState.Turn2)
                    {
                        moveDirection = transform.forward.normalized;
                        currentState = BounceState.Move;
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && currentState == BounceState.Move)
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflected = Vector3.Reflect(moveDirection, normal);

            // ù ��° ȸ�� ��ǥ: ���� ���⿡�� Y�� ���� +90��
            targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90f, 0f);
            currentState = BounceState.Turn1;
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
                Debug.Log("[BounceEnemy] �÷��̾� ���� ���� - ���� ����");
            }
            else
            {
                PlayerHealth.Instance.TakeDamage(attack);
            }
        }
    }

    private IEnumerator EnterPauseState()
    {
        currentState = BounceState.Pause;
        yield return new WaitForSeconds(pauseDuration);

        // �� ��° ȸ�� ��ǥ: ���� ���⿡�� Y�� ���� +90�� (�� 180��)
        targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + 90f, 0f);
        currentState = BounceState.Turn2;
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