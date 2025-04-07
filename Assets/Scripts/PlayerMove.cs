using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform model; // ȸ�� ��� ��
    [SerializeField] private GameObject normalModel; // �⺻ ���� ��
    [SerializeField] private GameObject dashModel;   // ��� ���� ��
    [SerializeField] private float moveSpeed = 5f; // �⺻ �̵� �ӵ�

    private Vector3 lastMoveDir = Vector3.forward; // ������ �̵� ����
    private float dashStartSpeed = 10f;     // ��� ���� �ӵ�
    private float dashAcceleration = 20f;   // �ʴ� ���ӷ�
    private float dashMaxSpeed = 50f;       // ��� �ִ� �ӵ�
    private bool isDashing = false;          // ��� ���� ����
    private float dashCurrentSpeed = 0f;     // ���� ��� �ӵ�
    private Vector3 dashDirection;           // ��� ���� (����)

    void Update()
    {
        if (isDashing)
        {
            // ��� �̵�
            transform.position += dashDirection * dashCurrentSpeed * Time.deltaTime;

            // ���� ó��
            dashCurrentSpeed += dashAcceleration * Time.deltaTime;
            dashCurrentSpeed = Mathf.Min(dashCurrentSpeed, dashMaxSpeed);

            return; // �⺻ �̵� ����
        }

        // �⺻ �̵� ó��
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);

        if (inputDir != Vector3.zero)
        {
            lastMoveDir = inputDir.normalized;
            transform.position += lastMoveDir * moveSpeed * Time.deltaTime;

            // �� Y�ุ ȸ�� (���� ����)
            Vector3 angles = model.localEulerAngles;
            angles.y = Mathf.Atan2(lastMoveDir.x, lastMoveDir.z) * Mathf.Rad2Deg;
            model.localEulerAngles = angles;
        }

        // ��� ���� ����
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
        Debug.Log("�浹 �õ��� with: " + collision.gameObject.name);

        if (!isDashing) return;

        // ��� �� �浹 �� ����
        isDashing = false;
        dashCurrentSpeed = 0f;

        normalModel.SetActive(true);
        dashModel.SetActive(false);

        Debug.Log("��� �� �浹: " + collision.gameObject.name);
    }
}
