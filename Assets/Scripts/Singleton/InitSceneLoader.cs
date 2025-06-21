using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneLoader : MonoBehaviour
{
    void Start()
    {
        // 필요한 싱글톤이 존재하지 않으면 생성
        /*
        if (GameManager.Instance == null)
        {
            GameObject go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
        */

        // 오디오 매니저 등 추가 가능

        // 1~2초 후 타이틀 씬으로 전환
        Invoke(nameof(GoToTitleScene), 1.5f);
    }

    void GoToTitleScene()
    {
        SceneManager.LoadScene("Title Scene");
    }
}
