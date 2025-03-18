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
        float h = Input.GetAxisRaw("Horizontal"); // Raw �Է� ���
        float v = Input.GetAxisRaw("Vertical");   // Raw �Է� ���

        Vector3 dir = new Vector3(h, 0, v).normalized;
        cc.Move(dir * playerMoveSpeed * Time.deltaTime);

        if (dir != Vector3.zero)
        {
            // �̵� ���⿡ ���� y�� ���� ��� (����->��)
            float targetYAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            // ���� localRotation�� ���� �״�� ������
            Vector3 localEuler = modelController.localEulerAngles;
            // y�ุ targetYAngle�� ����
            localEuler.y = targetYAngle;
            modelController.localEulerAngles = localEuler;
        }
    }
}
