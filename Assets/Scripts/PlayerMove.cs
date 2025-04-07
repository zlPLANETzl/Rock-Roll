using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform model; // 회전 대상 모델
    [SerializeField] private GameObject normalModel; // 기본 상태 모델
    [SerializeField] private GameObject dashModel;   // 대시 상태 모델
    [SerializeField] private float moveSpeed = 5f; // 기본 이동 속도

    private Vector3 lastMoveDir = Vector3.forward; // 마지막 이동 방향
    private float dashStartSpeed = 10f;     // 대시 시작 속도
    private float dashAcceleration = 20f;   // 초당 가속량
    private float dashMaxSpeed = 50f;       // 대시 최대 속도
    private bool isDashing = false;          // 대시 상태 여부
    private float dashCurrentSpeed = 0f;     // 현재 대시 속도
    private Vector3 dashDirection;           // 대시 방향 (고정)

    void Update()
    {
        if (isDashing)
        {
            // 대시 이동
            transform.position += dashDirection * dashCurrentSpeed * Time.deltaTime;

            // 가속 처리
            dashCurrentSpeed += dashAcceleration * Time.deltaTime;
            dashCurrentSpeed = Mathf.Min(dashCurrentSpeed, dashMaxSpeed);

            return; // 기본 이동 무시
        }

        // 기본 이동 처리
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);

        if (inputDir != Vector3.zero)
        {
            lastMoveDir = inputDir.normalized;
            transform.position += lastMoveDir * moveSpeed * Time.deltaTime;

            // 모델 Y축만 회전 (기울기 유지)
            Vector3 angles = model.localEulerAngles;
            angles.y = Mathf.Atan2(lastMoveDir.x, lastMoveDir.z) * Mathf.Rad2Deg;
            model.localEulerAngles = angles;
        }

        // 대시 시작 조건
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
            dashCurrentSpeed = dashStartSpeed;
            dashDirection = lastMoveDir;

            normalModel.SetActive(false);
            dashModel.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌 시도됨 with: " + collision.gameObject.name);

        if (!isDashing) return;

        // 대시 중 충돌 시 멈춤
        isDashing = false;
        dashCurrentSpeed = 0f;

        normalModel.SetActive(true);
        dashModel.SetActive(false);

        Debug.Log("대시 중 충돌: " + collision.gameObject.name);
    }
}
