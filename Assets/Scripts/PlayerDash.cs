using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float initialSpeed = 10f;           // ���� �ӵ� (�÷��̾� �⺻ �̵��ӵ� 5�� 2��)
    public float accelerationPerSec = 10f;     // �ʴ� ���ӵ�
    public float maxSpeed = 50f;               // �ִ� �ӵ�
    public float spinSpeed = 720f;             // ȸ�� �ӵ� (�����)

    private Vector3 direction;
    private float currentSpeed;
    private float elapsedTime;
    private System.Action<Vector3> onDashEnd;

    public void Init(Vector3 dir, System.Action<Vector3> callback)
    {
        direction = dir.normalized;
        onDashEnd = callback;
        currentSpeed = initialSpeed;
        elapsedTime = 0f;
    }

    void Update()
    {
        // ���� ó��
        elapsedTime += Time.deltaTime;
        currentSpeed = Mathf.Min(initialSpeed + accelerationPerSec * elapsedTime, maxSpeed);

        // �̵�
        transform.position += direction * currentSpeed * Time.deltaTime;

        // ȸ�� ���� (�������� ����)
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) return; // �ٴ� ��� ���� ����

        // ���̳� �ٸ� �浹 �� �÷��̾�� ����
        onDashEnd?.Invoke(transform.position);
        Destroy(gameObject);
    }
}