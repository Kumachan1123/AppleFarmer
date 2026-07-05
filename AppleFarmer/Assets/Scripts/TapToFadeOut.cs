using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// 画面をタップしたらフェードアウトを開始するクラス
/// </summary>
public class TapToFadeOut : MonoBehaviour
{
    /// <summary>
    /// フェード処理
    /// </summary>
    [SerializeField]
    private Fade m_fade;

    /// <summary>
    /// フェード開始済みフラグ
    /// </summary>
    private bool m_isFadeStarted = false;

    /// <summary>
    /// 毎フレーム更新
    /// </summary>
    private void Update()
    {
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                m_fade.FadeOut();
            }
        }

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                m_fade.FadeOut();
            }
        }
    }
}