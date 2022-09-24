using System.Collections;
using System.Linq;
using UnityEngine;

public class CanvasGroupAlphaAnimator : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AnimationCurve fadeInAnimationCurve;
    [SerializeField] AnimationCurve fadeOutAnimationCurve;
    public bool IsVisible = false;
    private void Awake()
    {
        if (IsVisible)
            canvasGroup.alpha = 1;
        else
            canvasGroup.alpha = 0;
    }
    public void FadeIn()
    {
        IsVisible = true;
        StopAllCoroutines();
        StartCoroutine(AnimationRoutine(fadeInAnimationCurve));
    }
    public void FadeOut()
    {
        IsVisible = false;
        StopAllCoroutines();
        StartCoroutine(AnimationRoutine(fadeOutAnimationCurve));
    }

    IEnumerator AnimationRoutine(AnimationCurve animationCurve)
    {
        var time = animationCurve.keys.First().time;
        var endTime = animationCurve.keys.Last().time;
        while (time < endTime)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = animationCurve.Evaluate(time);
            yield return null;
        }
        canvasGroup.alpha = animationCurve.keys.Last().value;
    }
}
