using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] private GameObject targetImage;

    public void ToggleImage()
    {
        if (targetImage != null)
        {
            bool nextState = !targetImage.activeSelf;
            targetImage.SetActive(nextState);
            Debug.Log($"[ImageToggleButton] �̹��� {(nextState ? "Ȱ��ȭ" : "��Ȱ��ȭ")}��");
        }
    }

    public void OnClickRetry()
    {
        Debug.Log("[GameOverButton] Retry Ŭ����");
        SceneManager.LoadScene("Game Scene");
    }

    public void OnClickQuit()
    {
        Debug.Log("[GameOverButton] Quit Ŭ����");
        SceneManager.LoadScene("Title Scene");
    }
}
