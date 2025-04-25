using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private GameObject rootObject;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI textGameOver;
    [SerializeField] private float fadeTime = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        rootObject.SetActive(false);
    }

    public void ShowGameOver(string message = "GAME OVER")
    {
        rootObject.SetActive(true);
        canvasGroup.alpha = 0f;

        textGameOver.text = message;
        textGameOver.transform.localScale = Vector3.zero;
        textGameOver.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        canvasGroup.DOFade(1f, fadeTime).SetEase(Ease.InOutQuad);
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
