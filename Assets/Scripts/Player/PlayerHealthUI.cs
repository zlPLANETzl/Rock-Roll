using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartAnchor;
    [SerializeField] private float heartSpacing = 0.5f;
    [SerializeField] private int maxHp = 3;

    private List<GameObject> heartInstances = new();

    void Awake()
    {
        for (int i = 0; i < maxHp; i++)
        {
            Vector3 offset = new Vector3(i * heartSpacing, 0f, 0f); // 왼쪽 정렬
            Quaternion rotation = Quaternion.Euler(50f, 0f, 0f); // x축으로 50도 회전
            GameObject heart = Instantiate(heartPrefab, heartAnchor.position + offset, rotation, heartAnchor);

            Vector3 originalScale = heartPrefab.transform.localScale; // 원래 프리팹 크기 사용
            heart.transform.localScale = Vector3.zero;
            heart.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);

            heartInstances.Add(heart);
        }
    }

    public void UpdateHeartDisplay(int currentHp)
    {
        for (int i = 0; i < heartInstances.Count; i++)
        {
            bool shouldBeVisible = i < currentHp;
            GameObject heart = heartInstances[i];

            if (shouldBeVisible && !heart.activeSelf)
            {
                heart.SetActive(true);
                heart.transform.localScale = Vector3.zero;
                heart.transform.DOScale(heartPrefab.transform.localScale, 0.3f).SetEase(Ease.OutBack);
            }
            else if (!shouldBeVisible && heart.activeSelf)
            {
                heart.SetActive(false);
            }
        }
    }
}