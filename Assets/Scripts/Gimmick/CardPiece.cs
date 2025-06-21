using UnityEngine;

public class CardPiece : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // 초기에는 움직이지 않음
    }

    public void Fall(Vector3 forceDir)
    {
        if (rb == null) return;

        rb.isKinematic = false;
        rb.AddForce(forceDir.normalized * 5f + Vector3.up * 2f, ForceMode.Impulse);

        // 필요 시 회전도 추가 가능
        rb.AddTorque(Random.onUnitSphere * 3f, ForceMode.Impulse);

        // 일정 시간 후 제거
        Destroy(gameObject, 2.5f);
    }
}
