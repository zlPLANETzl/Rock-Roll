using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private GameObject rootObject;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform imageGameOver;
    [SerializeField] private float fadeTime = 0.5f;

    private bool isShown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        rootObject.SetActive(false); // ���� ���� �� UI �����
    }

    public void ShowGameOver(string message = "GAME OVER")
    {
        if (isShown) return;
        isShown = true;

        Debug.Log("[GameOverUI] ShowGameOver �����");

        rootObject.SetActive(true);
        canvasGroup.alpha = 0f;

        if (imageGameOver != null)
        {
            imageGameOver.localScale = Vector3.zero;
            imageGameOver.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
        else
        {
            Debug.LogError("[GameOverUI] imageGameOver�� ������� �ʾҽ��ϴ�!");
        }

        canvasGroup.DOFade(1f, fadeTime).SetEase(Ease.InOutQuad);

        StartCoroutine(GoToTitleSceneAfterDelay(3f));
    }

    private IEnumerator GoToTitleSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Title Scene");
    }

    public void OnClickRetry()
    {
        Debug.Log("[GameOverUI] Retry ��ư Ŭ����");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickQuit()
    {
        Debug.Log("[GameOverUI] Quit ��ư Ŭ����");
        Application.Quit();
    }
}
