using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneLoader : MonoBehaviour
{
    void Start()
    {
        // �ʿ��� �̱����� �������� ������ ����
        /*
        if (GameManager.Instance == null)
        {
            GameObject go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
        */

        // ����� �Ŵ��� �� �߰� ����

        // 1~2�� �� Ÿ��Ʋ ������ ��ȯ
        Invoke(nameof(GoToTitleScene), 1.5f);
    }

    void GoToTitleScene()
    {
        SceneManager.LoadScene("Title Scene");
    }
}
