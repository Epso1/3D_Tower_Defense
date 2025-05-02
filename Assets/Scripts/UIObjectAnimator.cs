using UnityEngine;
using System.Collections;

public class UIObjectAnimator : MonoBehaviour
{
    [Header("Move In Animation Settings")]
    [SerializeField] private Vector3 inStartPosition;
    [SerializeField] private Vector3 inEndPosition;
    [SerializeField] private AnimationCurve inMoveCurve;
    [SerializeField] private float inTime;

    [Header("Move Out Animation Settings")]
    [SerializeField] private Vector3 outStartPosition;
    [SerializeField] private Vector3 outEndPosition;
    [SerializeField] private AnimationCurve outMoveCurve;
    [SerializeField] private float outTime;

    [Header("Scale In Animation Settings")]
    [SerializeField] private Vector3 inInitialScale;
    [SerializeField] private Vector3 inFinalScale;
    [SerializeField] private AnimationCurve inScaleCurve;
    [SerializeField] private float inScaleTime;
    [SerializeField] private float popOvershoot = 1.1f; // Factor de sobreimpulso

    [Header("Scale Out Animation Settings")]
    [SerializeField] private Vector3 outInitialScale;
    [SerializeField] private Vector3 outFinalScale;
    [SerializeField] private AnimationCurve outScaleCurve;
    [SerializeField] private float outScaleTime;

    public void MoveIn()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(inStartPosition, inEndPosition, inMoveCurve, inTime));
    }

    public void MoveOut()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(outStartPosition, outEndPosition, outMoveCurve, outTime));
    }

    public void ScaleIn()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(inInitialScale, inFinalScale, inScaleCurve, inScaleTime, popOvershoot));
    }

    public void ScaleOut()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(outInitialScale, outFinalScale, outScaleCurve, outScaleTime, 1.0f));
    }

    public void AnimateIn()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(inStartPosition, inEndPosition, inMoveCurve, inTime));
        StartCoroutine(ScaleCoroutine(inInitialScale, inFinalScale, inScaleCurve, inTime, popOvershoot));
    }

    public void AnimateOut()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(outStartPosition, outEndPosition, outMoveCurve, outTime));
        StartCoroutine(ScaleCoroutine(outInitialScale, outFinalScale, outScaleCurve, outTime, 1.0f));
    }

    private IEnumerator MoveCoroutine(Vector3 startPosition, Vector3 endPosition, AnimationCurve curve, float time)
    {
        transform.position = startPosition;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            float curveValue = curve.Evaluate(t);
            transform.position = Vector3.Lerp(startPosition, endPosition, curveValue);
            yield return null;
        }
        transform.position = endPosition;
    }

    private IEnumerator ScaleCoroutine(Vector3 initialScale, Vector3 finalScale, AnimationCurve curve, float time, float overshoot)
    {
        transform.localScale = initialScale;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            float curveValue = curve.Evaluate(t);
            float popFactor = (t < 0.75f) ? 1.0f + (overshoot - 1.0f) * Mathf.Sin(t * Mathf.PI) : 1.0f;
            transform.localScale = Vector3.Lerp(initialScale, finalScale * popFactor, curveValue);
            yield return null;
        }
        transform.localScale = finalScale;
    }
}
