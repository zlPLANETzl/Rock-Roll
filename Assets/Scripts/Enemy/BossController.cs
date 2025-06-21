using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    [Header("카드 공격 설정")]
    [SerializeField] private GameObject cardProjectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private Transform playerTarget;

    [Header("마술 모자 설정")]
    [SerializeField] private GameObject magicHatPrefab;
    [SerializeField] private float summonRadius = 30f;
    [SerializeField] private float summonHeight = 10f;
    [SerializeField] private LayerMask obstacleMask;

    [Header("보스 상태")]
    [SerializeField] private int maxHP = 3;
    [SerializeField] private Renderer bossRenderer;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 3;

    private int currentHP;
    private bool isAlive = true;
    private Color originalColor;

    private void Start()
    {
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                Transform target = playerObj.transform.Find("TargetPivot");
                if (target != null) playerTarget = target;
            }
        }

        if (bossRenderer != null)
        {
            originalColor = bossRenderer.material.color;
        }

        currentHP = maxHP;
        StartCoroutine(BossPatternRoutine());
    }

    private IEnumerator BossPatternRoutine()
    {
        while (isAlive)
        {
            yield return StartCoroutine(ExecuteCardPattern());
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(ExecuteSummonPattern());
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator ExecuteCardPattern()
    {
        if (cardProjectilePrefab == null || firePoint == null || playerTarget == null)
            yield break;

        for (int i = 0; i < 3; i++)
        {
            Vector3 startPos = firePoint.position;
            startPos.y = 1f;
            Vector3 direction = (playerTarget.position - startPos).normalized;

            GameObject proj = Instantiate(cardProjectilePrefab, startPos, Quaternion.LookRotation(direction));
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null) rb.velocity = direction * projectileSpeed;

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ExecuteSummonPattern()
    {
        if (magicHatPrefab == null)
            yield break;

        int count = 0;
        while (count < 3)
        {
            Vector2 randomXZ = Random.insideUnitCircle * summonRadius;
            Vector3 groundPos = transform.position + new Vector3(randomXZ.x, 0f, randomXZ.y);
            Vector3 spawnPos = groundPos + Vector3.up * summonHeight;

            if (!Physics.CheckSphere(groundPos, 1f, obstacleMask))
            {
                Instantiate(magicHatPrefab, spawnPos, Quaternion.identity);
                count++;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return null; // 다음 프레임에 재시도
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isAlive) return;

        currentHP -= amount;
        Debug.Log($"[Boss] 피해 {amount} → 남은 HP: {currentHP}");

        if (bossRenderer != null)
        {
            StartCoroutine(FlashBlink());
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashBlink()
    {
        for (int i = 0; i < flashCount; i++)
        {
            bossRenderer.material.color = hitColor;
            yield return new WaitForSeconds(flashDuration);
            bossRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private void Die()
    {
        isAlive = false;
        StopAllCoroutines();

        Debug.Log("[Boss] 사망 처리됨");
        if (GameOverUI.Instance != null)
        {
            GameOverUI.Instance.ShowGameOver("CLEAR");
        }

        Destroy(gameObject);
    }
}