using UnityEngine;
using UnityEngine.InputSystem;

public class Apple : MonoBehaviour
{
    public enum AppleState
    {
        Tree,
        Falling,
        Ground,
        Collected
    }

    public AppleState m_state;

    private Vector3 m_initPosition;

    private Rigidbody2D m_rb2d;

    private bool m_isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_state = AppleState.Tree;
        m_initPosition = transform.position;
        m_rb2d = GetComponent<Rigidbody2D>();
        // 初期状態では物理演算を切る
        m_rb2d.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case AppleState.Tree:// 木についてる
                // Handle tree m_state logic
                Tree();
                break;
            case AppleState.Falling:// 落下中
                // Handle falling m_state logic
                Fall();
                break;
            case AppleState.Ground:// 地面に落ちた
                // Handle ground m_state logic
                break;
            case AppleState.Collected:// 収穫済み
                // Handle collected m_state logic
                break;
        }
    }
    /// <summary>
    /// タップしてリンゴを木から落とす
    /// </summary>
    /// <summary>
    /// タップしてリンゴを木から落とす
    /// </summary>
    private void Tree()
    {
        bool tapped = false;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            tapped = true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            tapped = true;
        }

        if (!tapped)
        {
            return;
        }

        m_state = AppleState.Falling;

        // 物理演算開始
        m_rb2d.simulated = true;

        // 少し左右へ飛ばす
        m_rb2d.linearVelocity = new Vector2(
            Random.Range(-0.5f, 0.5f),
            0.0f);

        // 少し回転させる
        m_rb2d.angularVelocity = Random.Range(-180f, 180f);
    }
    /// <summary>
    /// 落下中
    /// </summary>
    private void Fall()
    {
        if (!m_isGrounded)
        {
            return;
        }

        // 着地したら停止
        m_rb2d.linearVelocity = Vector2.zero;
        m_rb2d.angularVelocity = 0.0f;
        m_rb2d.simulated = false;

        m_state = AppleState.Ground;
    }

    /// <summary>
    /// 衝突時
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_state != AppleState.Falling)
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Ground"))
        {
            return;
        }

        m_isGrounded = true;
    }
}
