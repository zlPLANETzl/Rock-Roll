using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GateWall[] gateWalls; // �ν����Ϳ��� ���
    [SerializeField] private GameObject bossObject;

    private List<GameObject> activeOrbs = new List<GameObject>();

    private void Awake()
    {
        // �̱��� ����: �� ������ ���������� ����
        Debug.Log("[GameManager] Awake ȣ��� - �� �� ������Ʈ�� ����");
    }

    // ���� ��� (���� ���� �� ȣ��)
    public void RegisterOrb(GameObject orb)
    {
        if (!activeOrbs.Contains(orb))
        {
            activeOrbs.Add(orb);
            Debug.Log($"���� ��ϵ�. ���� {activeOrbs.Count}��");
        }
    }

    // ������ ���ۿ� ����� �� ȣ��
    public void NotifyOrbRemoved(GameObject orb)
    {
        if (activeOrbs.Contains(orb))
        {
            activeOrbs.Remove(orb);
            Debug.Log($"���� ���ŵ�. ���� ��: {activeOrbs.Count}");

            if (activeOrbs.Count <= 0)
            {
                OnAllOrbsCleared();
            }
        }
    }

    // ��� ������ ���ŵǾ��� �� ȣ��Ǵ� �̺�Ʈ
    private void OnAllOrbsCleared()
    {
        Debug.Log("��� ������ ���ŵǾ����ϴ�!");
        foreach (GateWall wall in gateWalls)
        {
            wall.Disappear();
        }

        // ���� Ȱ��ȭ
        if (bossObject != null)
        {
            bossObject.SetActive(true);
        }
    }

    // ���¿� �Լ� (����)
    public void ResetOrbs()
    {
        activeOrbs.Clear();
        Debug.Log("���� ��� �ʱ�ȭ");
    }
}
