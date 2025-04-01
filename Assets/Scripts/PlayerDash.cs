using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float initialSpeed = 10f;           // 시작 속도 (플레이어 기본 이동속도 5의 2배)
    public float accelerationPerSec = 10f;     // 초당 가속도
    public float maxSpeed = 50f;               // 최대 속도
    public float spinSpeed = 720f;             // 회전 속도 (연출용)

    private Vector3 direction;
    private float currentSpeed;
    private float elapsedTime;
    private System.Action<Vector3> onDashEnd;

    public void Init(Vector3 dir, System.Action<Vector3> callback)
    {
        direction = dir.normalized;
        onDashEnd = callback;
        currentSpeed = initialSpeed;
        elapsedTime = 0f;
    }

    void Update()
    {
        // 가속 처리
        elapsedTime += Time.deltaTime;
        currentSpeed = Mathf.Min(initialSpeed + accelerationPerSec * elapsedTime, maxSpeed);

        // 이동
        transform.position += direction * currentSpeed * Time.deltaTime;

        // 회전 연출 (굴러가는 느낌)
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) return; // 바닥 통과 감지 방지

        // 벽이나 다른 충돌 시 플레이어로 복귀
        onDashEnd?.Invoke(transform.position);
        Destroy(gameObject);
    }
}