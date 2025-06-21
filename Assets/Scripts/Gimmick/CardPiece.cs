using UnityEngine;

public class CardPiece : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // �ʱ⿡�� �������� ����
    }

    public void Fall(Vector3 forceDir)
    {
        if (rb == null) return;

        rb.isKinematic = false;
        rb.AddForce(forceDir.normalized * 5f + Vector3.up * 2f, ForceMode.Impulse);

        // �ʿ� �� ȸ���� �߰� ����
        rb.AddTorque(Random.onUnitSphere * 3f, ForceMode.Impulse);

        // ���� �ð� �� ����
        Destroy(gameObject, 2.5f);
    }
}
