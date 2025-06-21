using System.Collections;
using UnityEngine;

public class MagicHatSpawner : MonoBehaviour
{
    [Header("소환 프리팹")]
    [SerializeField] private GameObject cardWallPrefab;
    [SerializeField] private GameObject healItemPrefab;
    [SerializeField] private GameObject summonEffectPrefab;

    [Header("설정")]
    [SerializeField] private float summonDelay = 1.0f;
    [SerializeField] private float selfDestructDelay = 2.0f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float fallSpeed = 10f;
    [SerializeField] private float groundY = 0.5f;

    private bool hasLanded = false;

    private void Update()
    {
        if (!hasLanded)
        {
            Vector3 targetPos = new Vector3(transform.position.x, groundY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, fallSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - groundY) < 0.01f)
            {
                hasLanded = true;
                StartCoroutine(SummonRoutine());
            }
        }
    }

    private IEnumerator SummonRoutine()
    {
        yield return new WaitForSeconds(summonDelay);

        if (Physics.CheckSphere(transform.position, 0.5f, obstacleMask))
        {
            Debug.Log("[MagicHat] 소환 실패: 이미 오브젝트 존재");
        }
        else
        {
            int choice = Random.Range(0, 3);
            switch (choice)
            {
                case 0:
                    Instantiate(cardWallPrefab, transform.position, Quaternion.identity);
                    Debug.Log("[MagicHat] 카드벽 생성");
                    break;
                case 1:
                    Instantiate(healItemPrefab, transform.position, Quaternion.identity);
                    Debug.Log("[MagicHat] 회복 아이템 생성");
                    break;
                case 2:
                    Debug.Log("[MagicHat] 아무것도 생성되지 않음");
                    break;
            }
        }

        if (summonEffectPrefab != null)
        {
            Instantiate(summonEffectPrefab, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(selfDestructDelay);
        Destroy(gameObject);
    }
}
