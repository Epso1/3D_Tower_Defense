using UnityEngine;
using System.Collections;

public class UIObjectAnimator : MonoBehaviour
{
    public enum MoveType { Lineal, Exponential, InverseExponential }
    public enum ScaleEffect { Lineal, PopIn, PopOut }

    [Header("Move In Animation Settings")]
    [SerializeField] private Vector3 inStartPosition;
    [SerializeField] private Vector3 inEndPosition;
    [SerializeField] private MoveType inMoveType;
    [SerializeField] private float inTime;

    [Header("Move Out Animation Settings")]
    [SerializeField] private Vector3 outStartPosition;
    [SerializeField] private Vector3 outEndPosition;
    [SerializeField] private MoveType outMoveType;
    [SerializeField] private float outTime;

    [Header("Scale In Animation Settings")]
    [SerializeField] private Vector3 inInitialScale;
    [SerializeField] private Vector3 inFinalScale;
    [SerializeField] private ScaleEffect inScaleEffect;
    [SerializeField] private float inScaleTime;

    [Header("Scale Out Animation Settings")]
    [SerializeField] private Vector3 outInitialScale;
    [SerializeField] private Vector3 outFinalScale;
    [SerializeField] private ScaleEffect outScaleEffect;
    [SerializeField] private float outScaleTime;

    public void MoveIn()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(inStartPosition, inEndPosition, inMoveType, inTime));
    }

    public void MoveOut()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(outStartPosition, outEndPosition, outMoveType, outTime));
    }

    public void ScaleIn()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(inInitialScale, inFinalScale, inScaleEffect, inScaleTime));
    }

    public void ScaleOut()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(outInitialScale, outFinalScale, outScaleEffect, outScaleTime));
    }

    public void AnimateIn()
    {
        StopAllCoroutines(); // Detiene cualquier animación previa
        StartCoroutine(MoveCoroutine(inStartPosition, inEndPosition, inMoveType, inTime));
        StartCoroutine(ScaleCoroutine(inInitialScale, inFinalScale, ScaleEffect.PopIn, inTime));
    }

    public void AnimateOut()
    {
        StopAllCoroutines(); // Detiene cualquier animación previa
        StartCoroutine(MoveCoroutine(outStartPosition, outEndPosition, outMoveType, outTime));
        StartCoroutine(ScaleCoroutine(outInitialScale, outFinalScale, ScaleEffect.PopOut, outTime));
    }


    private IEnumerator MoveCoroutine(Vector3 startPosition, Vector3 endPosition, MoveType moveType, float time)
    {
        transform.position = startPosition;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / time);

            switch (moveType)
            {
                case MoveType.Lineal:
                    break;
                case MoveType.Exponential:
                    t = 1 - Mathf.Exp(-6 * t);
                    break;
                case MoveType.InverseExponential:
                    t = Mathf.SmoothStep(0, 1, t);
                    break;
            }

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        transform.position = endPosition;
    }

    private IEnumerator ScaleCoroutine(Vector3 initialScale, Vector3 finalScale, ScaleEffect scaleEffect, float time)
    {
        transform.localScale = initialScale;
        float elapsed = 0f;
        float overshootFactor = 1.1f; // 10% más grande antes de estabilizarse

        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            Vector3 targetScale = finalScale;

            switch (scaleEffect)
            {
                case ScaleEffect.Lineal:
                    break;

                case ScaleEffect.PopIn:
                    if (t <= 0.6f)
                    {
                        t = Mathf.SmoothStep(0, 1, t / 0.6f); // Suavizar crecimiento
                        targetScale = Vector3.Lerp(initialScale, finalScale, t);
                    }
                    else if (t > 0.6f && t <= 0.9f)
                    {
                        float bounceT = (t - 0.6f) / 0.3f; // Normalizar a 0-1 en el tramo del rebote
                        targetScale = Vector3.Lerp(finalScale, finalScale * overshootFactor, bounceT);
                    }
                    else
                    {
                        float finalT = (t - 0.9f) / 0.1f;
                        targetScale = Vector3.Lerp(finalScale * overshootFactor, finalScale, Mathf.SmoothStep(0, 1, finalT));
                    }
                    break;

                case ScaleEffect.PopOut:
                    if (t <= 0.5f)
                    {
                        t = Mathf.SmoothStep(0, 1, t / 0.5f);
                        targetScale = Vector3.Lerp(initialScale, finalScale, t);
                    }
                    else if (t > 0.5f && t <= 0.75f)
                    {
                        float shrinkT = (t - 0.5f) / 0.3f;
                        targetScale = Vector3.Lerp(finalScale, finalScale * (1f - 0.1f), shrinkT);
                    }
                    else
                    {
                        float finalT = (t - 0.75f) / 0.2f;
                        targetScale = Vector3.Lerp(finalScale * (1f - 0.1f), finalScale, Mathf.SmoothStep(0, 1, finalT));
                    }
                    break;
            }

            transform.localScale = targetScale;
            yield return null;
        }

        transform.localScale = finalScale; // Asegurar tamaño final exacto
    }


}
