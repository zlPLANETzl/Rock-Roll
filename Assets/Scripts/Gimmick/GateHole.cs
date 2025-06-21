using UnityEngine;

public class GateHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            // ���� ��Ȱ��ȭ
            other.gameObject.SetActive(false);

            // GameManager�� �˸� (�� �� ������Ʈ Ž�� ���)
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.NotifyOrbRemoved(other.gameObject);
            }
            else
            {
                Debug.LogWarning("[GateHole] GameManager�� ã�� �� �����ϴ�.");
            }
        }
    }
}
