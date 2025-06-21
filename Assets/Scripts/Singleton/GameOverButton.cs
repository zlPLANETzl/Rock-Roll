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
            Debug.Log($"[ImageToggleButton] 이미지 {(nextState ? "활성화" : "비활성화")}됨");
        }
    }

    public void OnClickRetry()
    {
        Debug.Log("[GameOverButton] Retry 클릭됨");
        SceneManager.LoadScene("Game Scene");
    }

    public void OnClickQuit()
    {
        Debug.Log("[GameOverButton] Quit 클릭됨");
        SceneManager.LoadScene("Title Scene");
    }
}
