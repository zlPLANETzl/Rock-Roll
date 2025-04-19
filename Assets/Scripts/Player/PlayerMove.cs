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
    private float dashAcceleration = 80f;   // �ʴ� ���ӷ�
    private float dashMaxSpeed = 50f;       // ��� �ִ� �ӵ�
    private bool isDashing = false;          // ��� ���� ����
    private float dashCurrentSpeed = 0f;     // ���� ��� �ӵ�
    private Vector3 dashDirection;           // ��� ���� (����)

    // �ִ� �ӵ� Ȯ�ο� �ӽ� �ڵ�
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

            // �ִ� �ӵ� Ȯ�ο� �ӽ� �ڵ�
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
                Debug.Log("�̹� �浹 ���̶� ��� �Ұ�");
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
        Debug.Log("�浹 �õ��� with: " + collision.gameObject.name);

        if (!isDashing) return;

        // �������̽� ��� ��ȣ�ۿ� ó��
        if (collision.gameObject.TryGetComponent<IPlayerInteractable>(out var interactable))
        {
            interactable.OnPlayerDashEnter(this);
        }

        isDashing = false;
        dashCurrentSpeed = 0f;

        // �ִ� �ӵ� Ȯ�ο� �ӽ� �ڵ�
        dashRenderer.material.color = defaultColor;
        isColorChanged = false;

        normalModel.SetActive(true);
        dashModel.SetActive(false);

        Debug.Log("��� �� �浹: " + collision.gameObject.name);
    }

    public Vector3 GetLastMoveDirection()
    {
        return lastMoveDir;
    }
}
