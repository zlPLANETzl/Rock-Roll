using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerMoveSpeed = 7f;
    public Transform modelController;

    private CharacterController cc;

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

        Vector3 dir = new Vector3(h, 0, v).normalized;
        cc.Move(dir * playerMoveSpeed * Time.deltaTime);

        if (dir != Vector3.zero)
        {
            // 이동 방향에 따른 y축 각도 계산 (라디안->도)
            float targetYAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            // 기존 localRotation의 값을 그대로 가져옴
            Vector3 localEuler = modelController.localEulerAngles;
            // y축만 targetYAngle로 수정
            localEuler.y = targetYAngle;
            modelController.localEulerAngles = localEuler;
        }
    }
}
