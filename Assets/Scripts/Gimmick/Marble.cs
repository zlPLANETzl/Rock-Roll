using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private float shortPushSpeed = 15f;
    [SerializeField] private float fullPushSpeed = 40f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private Transform model;
    [SerializeField] private float rotationMultiplier = 15f;

    private Vector3 pushDirection;
    private float currentSpeed = 0f;
    private float minSpeed = 0f;
    private bool isMoving = false;

    private enum MarbleState { Idle, PushedShort, PushedFull }
    private MarbleState state = MarbleState.Idle;

    private Vector3 lastPosition;

    private void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.RegisterOrb(this.gameObject);
        }
        else
        {
            Debug.LogWarning("[Marble] GameManager를 찾을 수 없습니다.");
        }

        lastPosition = transform.position;
    }

    public void OnPlayerDashEnter(PlayerMove player)
    {
        Debug.Log("[Marble] OnPlayerDashEnter 호출됨");

        if (!player.IsDashing())
        {
            Debug.Log("[Marble] 플레이어 대시 상태 아님");
            return;
        }

        if (IsCurrentlyOverlapping())
        {
            Debug.Log("[Marble] 벽과 미차 상태에서 미기 무시됨");
            return;
        }

        float speed = player.GetDashSpeed();
        float max = player.GetDashMaxSpeed();
        pushDirection = player.GetLastMoveDirection().normalized;

        if (speed < max)
        {
            Debug.Log("[Marble] 약하게 밀림 시작");
            StartPush(shortPushSpeed, 0f, MarbleState.PushedShort);
        }
        else
        {
            Debug.Log("[Marble] 강하게 밀림 시작");
            StartPush(fullPushSpeed, 10f, MarbleState.PushedFull);
        }
    }

    private void StartPush(float startSpeed, float min, MarbleState newState)
    {
        transform.position += pushDirection * 0.2f;
        Debug.Log("[Marble] StartPush 호출됨, 방향: " + pushDirection);

        currentSpeed = startSpeed;
        minSpeed = min;
        isMoving = true;
        state = newState;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position += pushDirection * currentSpeed * Time.deltaTime;

            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed <= minSpeed)
            {
                currentSpeed = minSpeed;
                if (minSpeed == 0f)
                {
                    isMoving = false;
                    state = MarbleState.Idle;
                }
            }
        }

        // 이동량 기반 회전 연출 처리 (대시 외 직접 밀기 대응 포함)
        Vector3 delta = transform.position - lastPosition;
        float distance = delta.magnitude;
        if (distance > 0.001f && model != null)
        {
            Vector3 rotationAxis = -Vector3.Cross(delta.normalized, Vector3.up);
            float rotationAmount = distance * rotationMultiplier;
            model.Rotate(rotationAxis, rotationAmount, Space.World);
        }

        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isMoving) return;

        Debug.Log("[Marble] 충돌 발생: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player")) return;

        isMoving = false;
        currentSpeed = 0f;
        state = MarbleState.Idle;
    }

    private bool IsCurrentlyOverlapping()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hit in hits)
        {
            if (hit.gameObject != gameObject && hit.tag != "Ground" && hit.tag != "Player" && hit.tag != "Item")
            {
                return true;
            }
        }
        return false;
    }
}
