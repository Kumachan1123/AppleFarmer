using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{
    public enum FadeState { None, FadeIn, FadeOut }
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private FadeState initialState = FadeState.FadeIn;
    /// <summary> 
    /// /// フェード完了時に呼ばれるイベント 
    /// </summary> 
    public Action<FadeState> OnFadeCompleted;
    private Image image;
    private FadeState currentState;
    void Awake()
    {
        image = GetComponent<Image>();
        currentState = initialState;
        if (currentState == FadeState.FadeIn)
        {
            SetAlpha(1f);
        }
        else if (currentState == FadeState.FadeOut)
        {
            SetAlpha(0f);

        }
    }
    void Update()
    {
        if (image == null || currentState == FadeState.None || fadeDuration <= 0f)
        {
            return;
        }
        float targetAlpha = currentState == FadeState.FadeIn ? 0f : 1f;
        float newAlpha = Mathf.MoveTowards(image.color.a, targetAlpha, Time.deltaTime / fadeDuration);
        SetAlpha(newAlpha);
        if (Mathf.Approximately(newAlpha, targetAlpha))
        {
            FadeState completedState = currentState;
            currentState = FadeState.None; OnFadeCompleted?.Invoke(completedState);
        }
    }
    public void FadeIn()
    {
        // 黒から開始
        currentState = FadeState.FadeIn;
    }
    public void FadeOut()
    {
        // 透明から開始
        currentState = FadeState.FadeOut;
    }
    public void StopFade()
    {
        currentState = FadeState.None;
    }
    private void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
