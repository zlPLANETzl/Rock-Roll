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
    private float dashAcceleration = 80f;   // 초당 가속량
    private float dashMaxSpeed = 50f;       // 대시 최대 속도
    private bool isDashing = false;          // 대시 상태 여부
    private float dashCurrentSpeed = 0f;     // 현재 대시 속도
    private Vector3 dashDirection;           // 대시 방향 (고정)

    // 최대 속도 확인용 임시 코드
    [SerializeField] private Renderer dashRenderer;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color maxSpeedColor = Color.red;
    private bool isColorChanged = false;

    void Update()
    {
        if (isDashing)
        {
            transform.position += dashDirection * dashCurrentSpeed * Time.deltaTime;

            dashCurrentSpeed += dashAcceleration * Time.deltaTime;
            dashCurrentSpeed = Mathf.Min(dashCurrentSpeed, dashMaxSpeed);

            // 최대 속도 확인용 임시 코드
            if (dashCurrentSpeed >= dashMaxSpeed && !isColorChanged)
            {
                dashRenderer.material.color = maxSpeedColor;
                isColorChanged = true;
            }

            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);

        if (inputDir != Vector3.zero)
        {
            lastMoveDir = inputDir.normalized;
            transform.position += lastMoveDir * moveSpeed * Time.deltaTime;

            Vector3 angles = model.localEulerAngles;
            angles.y = Mathf.Atan2(lastMoveDir.x, lastMoveDir.z) * Mathf.Rad2Deg;
            model.localEulerAngles = angles;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsCurrentlyOverlapping())
            {
                Debug.Log("이미 충돌 중이라 대시 불가");
                return;
            }

            isDashing = true;
            dashCurrentSpeed = dashStartSpeed;
            dashDirection = lastMoveDir;

            normalModel.SetActive(false);
            dashModel.SetActive(true);
        }
    }

    private bool IsCurrentlyOverlapping()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1f);
        int count = 0;
        foreach (var hit in hits)
        {
            if (hit.gameObject != gameObject && hit.tag != "Ground")
            {
                count++;
            }
        }
        return count > 0;
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public float GetDashSpeed()
    {
        return dashCurrentSpeed;
    }

    public float GetDashMaxSpeed()
    {
        return dashMaxSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌 시도됨 with: " + collision.gameObject.name);

        if (!isDashing) return;

        // 인터페이스 기반 상호작용 처리
        if (collision.gameObject.TryGetComponent<IPlayerInteractable>(out var interactable))
        {
            interactable.OnPlayerDashEnter(this);
        }

        isDashing = false;
        dashCurrentSpeed = 0f;

        // 최대 속도 확인용 임시 코드
        dashRenderer.material.color = defaultColor;
        isColorChanged = false;

        normalModel.SetActive(true);
        dashModel.SetActive(false);

        Debug.Log("대시 중 충돌: " + collision.gameObject.name);
    }

    public Vector3 GetLastMoveDirection()
    {
        return lastMoveDir;
    }
}
