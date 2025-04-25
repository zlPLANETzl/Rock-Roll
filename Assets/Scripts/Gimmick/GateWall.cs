using UnityEngine;
using DG.Tweening;

public class GateWall : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f;

    private void Start()
    {
        // �ʱ⿣ ���� �ִٰ� GameManager���� �������� ȣ���
        gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }
               
}