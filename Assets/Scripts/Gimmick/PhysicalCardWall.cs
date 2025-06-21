using System.Collections.Generic;
using UnityEngine;

public class PhysicalCardWall : EnemyController, IDamageable
{
    [SerializeField] private List<CardPiece> cardPieces = new List<CardPiece>();

    void Start()
    {
        InitializeFromTable();
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"[PhysicalCardWall] 피해 {amount} → 남은 HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[PhysicalCardWall] 파괴됨 - 카드 조각 낙하 시작");

        // 콜라이더 비활성화로 플레이어 통과 가능하게
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Vector3 dashDir = FindObjectOfType<PlayerMove>().GetLastMoveDirection().normalized;

        foreach (var card in cardPieces)
        {
            card.Fall(dashDir);
        }

        Destroy(gameObject, 3f);
    }
}
