using UnityEngine;
using DG.Tweening;

public class GateWall : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f;

    private void Start()
    {
        // 초기엔 꺼져 있다가 GameManager에서 수동으로 호출됨
        gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }
               
}