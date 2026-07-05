using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// タップ位置へUIエフェクトを生成するクラス
/// </summary>
public class TapEffectSpawner : MonoBehaviour
{
    /// <summary>
    /// 波紋エフェクトPrefab
    /// </summary>
    [SerializeField]
    private RectTransform m_effectPrefab;

    /// <summary>
    /// エフェクトを配置する親
    /// </summary>
    [SerializeField]
    private RectTransform m_effectRoot;

    /// <summary>
    /// 親Canvas
    /// </summary>
    [SerializeField]
    private Canvas m_canvas;

    /// <summary>
    /// 毎フレーム更新
    /// </summary>
    private void Update()
    {
        if (!TryGetTapPosition(out Vector2 screenPosition))
        {
            return;
        }

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_effectRoot,
                screenPosition,
                m_canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : m_canvas.worldCamera,
                out Vector2 localPosition))
        {
            return;
        }

        RectTransform effect = Instantiate(m_effectPrefab, m_effectRoot);

        effect.anchoredPosition = localPosition;

        // 一番手前に表示
        effect.SetAsLastSibling();
    }

    /// <summary>
    /// タップ位置を取得する
    /// </summary>
    private bool TryGetTapPosition(out Vector2 position)
    {
        position = default;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            position = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            position = Mouse.current.position.ReadValue();
            return true;
        }

        return false;
    }
}