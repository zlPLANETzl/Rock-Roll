using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy ��Ʈ ID (EnemyTable�� ��Ī)")]
    [Tooltip("UGS EnemyTable���� �ش��ϴ� enemyId�� �����͸� �ε��մϴ�.")]
    public int enemyId;

    [Header("�ڵ����� ���õǴ� �� (�б� ����)")]
    public int hp;
    public int attack;
    public float speed;

    void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    /// <summary>
    /// ��Ʈ ������ ������� hp, attack, speed�� �ʱ�ȭ�մϴ�.
    /// </summary>
    public virtual void InitializeFromTable()
    {
        if (!EnemyTable.Data.DataMap.ContainsKey(enemyId))
        {
            Debug.LogWarning($"[EnemyController] ID {enemyId} ������ ����");
            return;
        }

        var data = EnemyTable.Data.DataMap[enemyId];
        hp = data.hp;
        attack = data.attack;
        speed = data.speed;

        Debug.Log($"[EnemyController] �ʱ�ȭ �Ϸ�: ID={enemyId}, HP={hp}, ATK={attack}, SPD={speed}");
    }
}
