using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移を管理するクラス
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// フェード処理
    /// </summary>
    [SerializeField]
    private Fade m_fade;

    /// <summary>
    /// 次に遷移するシーン名
    /// </summary>
    private string m_nextSceneName = string.Empty;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        if (m_fade != null)
        {
            m_fade.OnFadeCompleted += OnFadeCompleted;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    private void OnDestroy()
    {
        if (m_fade != null)
        {
            m_fade.OnFadeCompleted -= OnFadeCompleted;
        }
    }

    /// <summary>
    /// フェードアウト付きでシーン遷移する
    /// </summary>
    /// <param name="sceneName">遷移先シーン名</param>
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        m_nextSceneName = sceneName;

        m_fade.FadeOut();
    }

    /// <summary>
    /// フェード終了時
    /// </summary>
    /// <param name="state">終了したフェード状態</param>
    private void OnFadeCompleted(Fade.FadeState state)
    {
        if (state != Fade.FadeState.FadeOut || m_nextSceneName == "")
        {
            return;
        }

        SceneManager.LoadScene(m_nextSceneName);
    }
}