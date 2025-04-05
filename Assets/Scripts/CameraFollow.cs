using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target; // 플레이어
    private float normalSpeed = 5f;
    private float maxDashFollowSpeed = 60f;
    private float dashAcceleration = 30f;
    private float currentSpeed;
    private Vector3 offset;
    private PlayerMove playerMove; // 플레이어 상태 확인용

    void Start()
    {
        offset = transform.position - target.position;
        playerMove = target.GetComponent<PlayerMove>();
        currentSpeed = normalSpeed;
    }

    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 direction = desiredPos - transform.position;

        if (playerMove.isDashing)
        {
            // 대시 중 → 카메라 속도 점점 증가
            currentSpeed = Mathf.Min(currentSpeed + dashAcceleration * Time.deltaTime, maxDashFollowSpeed);
        }
        else
        {
            // 일반 상태 → 즉시 기본 속도로 복귀
            currentSpeed = normalSpeed;
        }

        // 카메라 이동
        transform.position += direction.normalized * currentSpeed * Time.deltaTime;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
    }
}