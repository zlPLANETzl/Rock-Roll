using UnityEngine;

public class GateHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            // 구슬 비활성화
            other.gameObject.SetActive(false);

            // GameManager에 알림
            GameManager.Instance.NotifyOrbRemoved(other.gameObject);
        }
    }
}