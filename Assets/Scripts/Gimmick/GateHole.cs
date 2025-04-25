using UnityEngine;

public class GateHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            // ���� ��Ȱ��ȭ
            other.gameObject.SetActive(false);

            // GameManager�� �˸�
            GameManager.Instance.NotifyOrbRemoved(other.gameObject);
        }
    }
}