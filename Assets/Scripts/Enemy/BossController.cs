using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 3;

    public GameObject projectilePrefab; // Enemy 프리팹
    public Transform firePoint;
    public float fireInterval = 2f;
    private float fireTimer;

    
    public int hp = 3;

    void Start()
    {
        fireTimer = fireInterval;
    }

    void Update()
    {
        AttackPattern();
    }   

    private void AttackPattern()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireProjectile();
            fireTimer = fireInterval;
        }
    }

    private void FireProjectile()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // Y축 제외하고 수평 방향만 계산
        Vector3 targetPos = player.transform.position;
        targetPos.y = firePoint.position.y;

        Vector3 dir = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(dir));

        if (proj.TryGetComponent<BossProjectile>(out var bp))
        {
            bp.SetDirection(dir);
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        FlashOnHit();

        if (hp <= 0)
        {
            Die();
        }
    }

    private void FlashOnHit()
    {
        foreach (var renderer in renderers)
        {
            Color originalColor = renderer.material.color;
            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < flashCount; i++)
            {
                seq.Append(renderer.material.DOColor(flashColor, flashDuration / 2f));
                seq.Append(renderer.material.DOColor(originalColor, flashDuration / 2f));
            }
        }
    }

    private void Die()
    {
        Debug.Log("보스 사망! 스테이지 클리어!");
        GameOverUI.Instance.ShowGameOver("CLEAR");
        Destroy(gameObject);        
    }
}
