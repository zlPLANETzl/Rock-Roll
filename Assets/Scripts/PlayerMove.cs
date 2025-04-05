using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isDashing = false;

    [SerializeField]
    private Transform modelController;
    [SerializeField]
    private GameObject dashBallPrefab;
    
    private float playerMoveSpeed = 5f;
    private Vector3 lastMoveDir = Vector3.forward;
    private CharacterController cc;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // Raw �Է� ���
        float v = Input.GetAxisRaw("Vertical");   // Raw �Է� ���

        Vector3 inputDir = new Vector3(h, 0, v).normalized;
        cc.Move(inputDir * playerMoveSpeed * Time.deltaTime);

        if (inputDir != Vector3.zero)
        {
            // �̵� ���⿡ ���� y�� ���� ��� (����->��)
            float targetYAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            // ���� localRotation�� ���� �״�� ������
            Vector3 localEuler = modelController.localEulerAngles;
            // y�ุ targetYAngle�� ����
            localEuler.y = targetYAngle;
            modelController.localEulerAngles = localEuler;
        }

        if (inputDir != Vector3.zero)
        {
            lastMoveDir = inputDir.normalized;
            // �̵� �� ȸ�� ó��
        }

        // �뽬 ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        gameObject.SetActive(false);

        GameObject ball = Instantiate(dashBallPrefab, transform.position + Vector3.up * 1.5f, Quaternion.LookRotation(lastMoveDir));
        ball.GetComponent<PlayerDash>().Init(lastMoveDir, OnDashEnd);

        Camera.main.GetComponent<CameraFollow>().SetTarget(ball.transform);
    }

    void OnDashEnd(Vector3 returnPosition)
    {
        returnPosition.y -= 1.5f; // Y ����

        transform.position = returnPosition;
        gameObject.SetActive(true);
        isDashing = false;

        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
    }
}
