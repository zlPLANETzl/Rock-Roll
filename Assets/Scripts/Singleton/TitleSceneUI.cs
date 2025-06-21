using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleSceneUI : MonoBehaviour
{
    [Header("UI ���")]
    [SerializeField] private RectTransform logoImage;
    [SerializeField] private Button startButton;
    [SerializeField] private float pulseScale = 1.1f;
    [SerializeField] private float pulseDuration = 0.8f;

    private Tween logoTween;

    private void Start()
    {
        // �ΰ� �̹��� �ݺ� �ִϸ��̼�
        logoTween = logoImage.DOScale(pulseScale, pulseDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // ��ư Ŭ�� �� GameScene���� �̵�
        startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        logoTween?.Kill(); // �����ϰ� Tween ����
        SceneManager.LoadScene("GameScene");
    }
}
