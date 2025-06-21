using System.Collections;
using UnityEngine;

public class MagicHatSpawner : MonoBehaviour
{
    [Header("��ȯ ������")]
    [SerializeField] private GameObject cardWallPrefab;
    [SerializeField] private GameObject healItemPrefab;
    [SerializeField] private GameObject summonEffectPrefab;

    [Header("����")]
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
            Debug.Log("[MagicHat] ��ȯ ����: �̹� ������Ʈ ����");
        }
        else
        {
            int choice = Random.Range(0, 3);
            switch (choice)
            {
                case 0:
                    Instantiate(cardWallPrefab, transform.position, Quaternion.identity);
                    Debug.Log("[MagicHat] ī�庮 ����");
                    break;
                case 1:
                    Instantiate(healItemPrefab, transform.position, Quaternion.identity);
                    Debug.Log("[MagicHat] ȸ�� ������ ����");
                    break;
                case 2:
                    Debug.Log("[MagicHat] �ƹ��͵� �������� ����");
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
