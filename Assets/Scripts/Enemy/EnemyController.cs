using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy 시트 ID (EnemyTable와 매칭)")]
    [Tooltip("UGS EnemyTable에서 해당하는 enemyId의 데이터를 로드합니다.")]
    public int enemyId;

    [Header("자동으로 세팅되는 값 (읽기 전용)")]
    public int hp;
    public int attack;
    public float speed;

    void Awake()
    {
        UnityGoogleSheet.LoadAllData();
    }

    /// <summary>
    /// 시트 데이터 기반으로 hp, attack, speed를 초기화합니다.
    /// </summary>
    public virtual void InitializeFromTable()
    {
        if (!EnemyTable.Data.DataMap.ContainsKey(enemyId))
        {
            Debug.LogWarning($"[EnemyController] ID {enemyId} 데이터 없음");
            return;
        }

        var data = EnemyTable.Data.DataMap[enemyId];
        hp = data.hp;
        attack = data.attack;
        speed = data.speed;

        Debug.Log($"[EnemyController] 초기화 완료: ID={enemyId}, HP={hp}, ATK={attack}, SPD={speed}");
    }
}
