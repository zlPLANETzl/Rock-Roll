using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class EnemyTableLogger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("<color=cyan>[EnemyTableLogger]</color> 시트 로딩 시작");

        var list = EnemyTable.Data.DataList;

        Debug.Log($"로드된 적 개수: {list.Count}");

        foreach (var enemy in list)
        {
            Debug.Log($"[EnemyTable] ID: {enemy.id}, Name: {enemy.name}, " +
                      $"Type: {enemy.type}, HP: {enemy.hp}, Attack: {enemy.attack}, " +
                      $"Speed: {enemy.speed}, AI: {enemy.aiType}, Prefab: {enemy.prefab}");
        }

        Debug.Log("<color=green>[EnemyTableLogger]</color> 시트 로딩 완료");
    }
}
