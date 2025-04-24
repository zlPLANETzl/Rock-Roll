using UnityEngine;
using DG.Tweening;

public class HealItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private float rotateDuration = 2f;
    [SerializeField] private float floatHeight = 0.25f;
    [SerializeField] private float floatDuration = 1f;

    private void Start()
    {
        // Y축 회전
        transform.DORotate(new Vector3(0, 360, 0), rotateDuration, RotateMode.FastBeyond360)
                 .SetLoops(-1, LoopType.Restart)
                 .SetEase(Ease.Linear);

        // 위아래 부유
        float originalY = transform.position.y;
        transform.DOMoveY(originalY + floatHeight, floatDuration)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.Instance.Heal(healAmount);

            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // DOTween 정리 후 비활성화 (풀링 구조 대비)
            transform.DOKill(true);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.DOKill(); // 예외 상황 대비
    }
}
