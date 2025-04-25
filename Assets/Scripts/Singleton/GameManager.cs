using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GateWall[] gateWalls; // 인스펙터에서 등록
    [SerializeField] private GameObject bossObject;
    public static GameManager Instance { get; private set; }

    private List<GameObject> activeOrbs = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // InitScene 구조라면 유지
    }

    // 구슬 등록 (게임 시작 시 호출)
    public void RegisterOrb(GameObject orb)
    {
        if (!activeOrbs.Contains(orb))
        {
            activeOrbs.Add(orb);
            Debug.Log($"구슬 등록됨. 현재 {activeOrbs.Count}개");
        }
    }

    // 구슬이 구멍에 닿았을 때 호출
    public void NotifyOrbRemoved(GameObject orb)
    {
        if (activeOrbs.Contains(orb))
        {
            activeOrbs.Remove(orb);
            Debug.Log($"구슬 제거됨. 남은 수: {activeOrbs.Count}");

            if (activeOrbs.Count <= 0)
            {
                OnAllOrbsCleared();
            }
        }
    }

    // 모든 구슬이 제거되었을 때 호출되는 이벤트
    private void OnAllOrbsCleared()
    {
        Debug.Log("모든 구슬이 제거되었습니다!");
        foreach (GateWall wall in gateWalls)
        {
            wall.Disappear();
        }

        // 보스 활성화
        if (bossObject != null)
        {
            bossObject.SetActive(true);
        }
    }

    // 리셋용 함수 (선택)
    public void ResetOrbs()
    {
        activeOrbs.Clear();
        Debug.Log("구슬 목록 초기화");
    }
}