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
        Debug.Log($"[PhysicalCardWall] ���� {amount} �� ���� HP: {hp}");
        if (hp <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        Debug.Log("[PhysicalCardWall] �ı��� - ī�� ���� ���� ����");

        // �ݶ��̴� ��Ȱ��ȭ�� �÷��̾� ��� �����ϰ�
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
