using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 波紋エフェクトを管理するクラス
/// </summary>
[RequireComponent(typeof(Image))]
public class RippleEffect : MonoBehaviour
{
    /// <summary>
    /// エフェクトの生存時間
    /// </summary>
    [SerializeField]
    private float m_lifeTime = 1.5f;

    /// <summary>
    /// Imageコンポーネント
    /// </summary>
    private Image m_image;

    /// <summary>
    /// 実体化したマテリアル
    /// </summary>
    private Material m_materialInstance;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        m_image = GetComponent<Image>();

        // マテリアルを複製
        m_materialInstance = new Material(m_image.material);

        // Imageへ設定
        m_image.material = m_materialInstance;
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    private void OnEnable()
    {
        // 波紋開始時間を更新
        m_materialInstance.SetFloat("_StartTime", Time.time);

        // 一定時間後に削除
        Destroy(gameObject, m_lifeTime);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    private void OnDestroy()
    {
        if (m_materialInstance != null)
        {
            Destroy(m_materialInstance);
        }
    }
}