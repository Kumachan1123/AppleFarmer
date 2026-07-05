using System.Collections;
using UnityEngine;

/// <summary>
/// ロゴシーンを管理するクラス
/// </summary>
public class LogoScene : MonoBehaviour
{
    /// <summary>
    /// シーン遷移管理
    /// </summary>
    [SerializeField]
    private SceneLoader m_sceneLoader;

    /// <summary>
    /// ロゴ表示時間
    /// </summary>
    [SerializeField]
    private float m_logoDisplayTime = 2.0f;

    /// <summary>
    /// 次に遷移するシーン名
    /// </summary>
    [SerializeField]
    private string m_nextSceneName = string.Empty;
    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        StartCoroutine(LogoSequence());
    }

    /// <summary>
    /// ロゴ表示シーケンス
    /// </summary>
    private IEnumerator LogoSequence()
    {
        // フェードインが終わるまで待つ
        yield return new WaitForSeconds(1.0f);

        // ロゴを表示
        yield return new WaitForSeconds(m_logoDisplayTime);

        // タイトルへ遷移
        m_sceneLoader.LoadScene(m_nextSceneName);
    }
}