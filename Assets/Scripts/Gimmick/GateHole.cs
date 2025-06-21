using UnityEngine;

public class GateHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            // 구슬 비활성화
            other.gameObject.SetActive(false);

            // GameManager에 알림 (씬 내 오브젝트 탐색 방식)
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.NotifyOrbRemoved(other.gameObject);
            }
            else
            {
                Debug.LogWarning("[GateHole] GameManager를 찾을 수 없습니다.");
            }
        }
    }
}
