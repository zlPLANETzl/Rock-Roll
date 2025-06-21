using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private GameObject dashModel;
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 lastMoveDir = Vector3.back;
    private float dashStartSpeed = 10f;
    private float dashAcceleration = 80f;
    private float dashMaxSpeed = 50f;
    private bool isDashing = false;
    private float dashCurrentSpeed = 0f;
    private Vector3 dashDirection;

    [SerializeField] private Renderer dashRenderer;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color maxSpeedColor = Color.red;
    private bool isColorChanged = false;

    private bool isInvincible = false;

    [SerializeField] private float rotationMultiplier = 10f;

    void Update()
    {
        if (isDashing)
        {
            transform.position += dashDirection * dashCurrentSpeed * Time.deltaTime;

            dashCurrentSpeed += dashAcceleration * Time.deltaTime;
            dashCurrentSpeed = Mathf.Min(dashCurrentSpeed, dashMaxSpeed);

            if (dashCurrentSpeed >= dashMaxSpeed && !isColorChanged)
            {
                dashRenderer.material.color = maxSpeedColor;
                isColorChanged = true;
            }

            // ��� �� ���� ���� ȸ�� ó��
            float rotationSpeed = dashCurrentSpeed * rotationMultiplier;
            dashModel.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);

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

            // ȸ�� �ʱ�ȭ �� ��� ���� ����
            dashModel.transform.localRotation = Quaternion.identity;
            dashModel.transform.rotation = Quaternion.LookRotation(dashDirection, Vector3.up);
            dashModel.SetActive(true);
        }
    }

    private bool IsCurrentlyOverlapping()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.8f);
        foreach (var hit in hits)
        {
            if (hit.gameObject != gameObject && hit.tag != "Ground")
            {
                return true;
            }
        }
        return false;
    }

    public bool IsDashing() => isDashing;
    public float GetDashSpeed() => dashCurrentSpeed;
    public float GetDashMaxSpeed() => dashMaxSpeed;
    public bool IsAtMaxDashSpeed() => dashCurrentSpeed >= dashMaxSpeed;
    public Vector3 GetLastMoveDirection() => lastMoveDir;
    public bool IsInvincible() => isInvincible;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹 �õ��� with: " + collision.gameObject.name);

        if (!isDashing) return;

        if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            if (IsAtMaxDashSpeed())
            {
                Debug.Log("[Player] �ִ� �ӵ��� �浹 - ���� ó�� �õ�");
                damageable.TakeDamage(1);
            }
            else
            {
                Debug.Log("[Player] �ִ� �ӵ� �ƴ� - ���� ����");
            }
        }

        if (collision.gameObject.TryGetComponent<IPlayerInteractable>(out var interactable))
        {
            interactable.OnPlayerDashEnter(this);
        }

        isDashing = false;
        dashCurrentSpeed = 0f;
        StartCoroutine(TemporaryInvincibility(0.2f));

        dashRenderer.material.color = defaultColor;
        isColorChanged = false;

        dashModel.SetActive(false);

        Debug.Log("��� �� �浹: " + collision.gameObject.name);
    }

    private IEnumerator TemporaryInvincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}
