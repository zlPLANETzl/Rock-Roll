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

        Vector3 dashDir = transform.forward;
        Vector3 spawnPos = transform.position;

        // �÷��̾� ��Ȱ��ȭ
        gameObject.SetActive(false);

        // �� ������ ����
        Vector3 spawnOffset = new Vector3(0, 1.5f, 0); // Y������ +1.5
        GameObject ball = Instantiate(dashBallPrefab, transform.position + spawnOffset, Quaternion.LookRotation(lastMoveDir));
        ball.GetComponent<PlayerDash>().Init(lastMoveDir, OnDashEnd);
    }

    // �ݹ����� �÷��̾� ����
    void OnDashEnd(Vector3 returnPosition)
    {
        transform.position = returnPosition;
        gameObject.SetActive(true);
        isDashing = false;
    }
}
