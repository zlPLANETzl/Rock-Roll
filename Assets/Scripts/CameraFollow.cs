using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target; // �÷��̾�
    private float normalSpeed = 5f;
    private float maxDashFollowSpeed = 60f;
    private float dashAcceleration = 30f;
    private float currentSpeed;
    private Vector3 offset;
    private PlayerMove playerMove; // �÷��̾� ���� Ȯ�ο�

    void Start()
    {
        offset = transform.position - target.position;
        playerMove = target.GetComponent<PlayerMove>();
        currentSpeed = normalSpeed;
    }

    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 direction = desiredPos - transform.position;

        if (playerMove.isDashing)
        {
            // ��� �� �� ī�޶� �ӵ� ���� ����
            currentSpeed = Mathf.Min(currentSpeed + dashAcceleration * Time.deltaTime, maxDashFollowSpeed);
        }
        else
        {
            // �Ϲ� ���� �� ��� �⺻ �ӵ��� ����
            currentSpeed = normalSpeed;
        }

        // ī�޶� �̵�
        transform.position += direction.normalized * currentSpeed * Time.deltaTime;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
    }
}