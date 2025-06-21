using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleSceneUI : MonoBehaviour
{
    [Header("UI 요소")]
    [SerializeField] private RectTransform logoImage;
    [SerializeField] private Button startButton;
    [SerializeField] private float pulseScale = 1.1f;
    [SerializeField] private float pulseDuration = 0.8f;

    private Tween logoTween;

    private void Start()
    {
        // 로고 이미지 반복 애니메이션
        logoTween = logoImage.DOScale(pulseScale, pulseDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // 버튼 클릭 시 GameScene으로 이동
        startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        logoTween?.Kill(); // 안전하게 Tween 종료
        SceneManager.LoadScene("GameScene");
    }
}
