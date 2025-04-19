using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private float shortPushSpeed = 20f;
    [SerializeField] private float fullPushSpeed = 20f;
    [SerializeField] private float deceleration = 10f;

    private Vector3 pushDirection;
    private float currentSpeed = 0f;
    private float minSpeed = 0f;
    private bool isMoving = false;

    private enum MarbleState { Idle, PushedShort, PushedFull }
    private MarbleState state = MarbleState.Idle;

    public void OnPlayerDashEnter(PlayerMove player)
    {
        Debug.Log("[Marble] OnPlayerDashEnter 호출됨");

        if (!player.IsDashing())
        {
            Debug.Log("[Marble] 플레이어 대시 상태 아님");
            return;
        }

        float speed = player.GetDashSpeed();
        float max = player.GetDashMaxSpeed();
        pushDirection = player.GetLastMoveDirection().normalized;

        if (speed < max)
        {
            Debug.Log("[Marble] 약하게 밀림 시작");
            StartPush(shortPushSpeed, 0f, MarbleState.PushedShort); // 1초 후 정지
        }
        else
        {
            Debug.Log("[Marble] 강하게 밀림 시작");
            StartPush(fullPushSpeed, 10f, MarbleState.PushedFull); // 1초 후 감속해도 10 유지
        }
    }

    private void StartPush(float startSpeed, float min, MarbleState newState)
    {
        transform.position += pushDirection * 0.2f; // 충돌 해소용 살짝 이동
        Debug.Log("[Marble] StartPush 호출됨, 방향: " + pushDirection);

        currentSpeed = startSpeed;
        minSpeed = min;
        isMoving = true;
        state = newState;
    }

    private void Update()
    {
        if (!isMoving) return;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (!isMoving) return;

        Debug.Log("[Marble] 충돌 발생: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player")) return;

        if (state == MarbleState.PushedFull)
        {
            isMoving = false;
            currentSpeed = 0f;
            state = MarbleState.Idle;
        }
    }
}