using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerMoveSpeed = 5f;
    public Transform modelController;
    public GameObject dashBallPrefab;

    private Vector3 lastMoveDir = Vector3.forward;
    private CharacterController cc;
    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // Raw 입력 사용
        float v = Input.GetAxisRaw("Vertical");   // Raw 입력 사용

        Vector3 inputDir = new Vector3(h, 0, v).normalized;
        cc.Move(inputDir * playerMoveSpeed * Time.deltaTime);

        if (inputDir != Vector3.zero)
        {
            // 이동 방향에 따른 y축 각도 계산 (라디안->도)
            float targetYAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            // 기존 localRotation의 값을 그대로 가져옴
            Vector3 localEuler = modelController.localEulerAngles;
            // y축만 targetYAngle로 수정
            localEuler.y = targetYAngle;
            modelController.localEulerAngles = localEuler;
        }

        if (inputDir != Vector3.zero)
        {
            lastMoveDir = inputDir.normalized;
            // 이동 및 회전 처리
        }

        // 대쉬 시작
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;

        Vector3 dashDir = transform.forward;
        Vector3 spawnPos = transform.position;

        // 플레이어 비활성화
        gameObject.SetActive(false);

        // 공 프리팹 생성
        Vector3 spawnOffset = new Vector3(0, 1.5f, 0); // Y축으로 +1.5
        GameObject ball = Instantiate(dashBallPrefab, transform.position + spawnOffset, Quaternion.LookRotation(lastMoveDir));
        ball.GetComponent<PlayerDash>().Init(lastMoveDir, OnDashEnd);
    }

    // 콜백으로 플레이어 복귀
    void OnDashEnd(Vector3 returnPosition)
    {
        transform.position = returnPosition;
        gameObject.SetActive(true);
        isDashing = false;
    }
}
