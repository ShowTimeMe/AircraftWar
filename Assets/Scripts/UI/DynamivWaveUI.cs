using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 敌人波数UI动态显示类
/// </summary>
public class DynamivWaveUI : MonoBehaviour
{
    #region FIELDS
    [SerializeField]float animationTime = 1f;
    
    [Header("Line Move")]
    [SerializeField] Vector2 lineTopStartPosition = new Vector2(-1250f, 50f);
    [SerializeField] Vector2 lineTopTargetPosition = new Vector2(0f, 50f);
    [SerializeField] Vector2 lineBottomStartPosition = new Vector2(1250f, -50f);
    [SerializeField] Vector2 lineBottomTargetPosition = new Vector2(0f, -50f);

    [Header("Text Scale")]
    [SerializeField] Vector2 waveTextStartScale=new Vector2(1f,0f);
    [SerializeField] Vector2 waveTextTargettScale = new Vector2(1f, 1f);

    RectTransform lineTop;
    RectTransform lineBottom;
    RectTransform waveText;

    WaitForSeconds waitStayTime;

    #endregion

    #region Unity Event Functions
    private void Awake()
    {
        //如果使用动画控制删除脚本
        if (TryGetComponent<Animator>(out Animator animator))
        {
            if (animator.isActiveAndEnabled)
            {
                Destroy(this);
            }
        }
        waitStayTime = new WaitForSeconds(EnemyManager.Instance.TimeBetweenWaves - animationTime * 2f);

        lineTop = transform.Find("LineTop").GetComponent<RectTransform>();
        lineBottom = transform.Find("LineBottom").GetComponent<RectTransform>();
        waveText = transform.Find("LineTop").GetComponent<RectTransform>();

        lineTop.localPosition = lineTopStartPosition;
        lineBottom.localPosition = lineBottomStartPosition;
        waveText.localScale = waveTextStartScale;
    }

    private void OnEnable()
    {
        StartCoroutine(LineMoveCoroutine(lineTop, lineTopTargetPosition, lineTopStartPosition));
        StartCoroutine(LineMoveCoroutine(lineBottom, lineBottomTargetPosition, lineBottomStartPosition));
        StartCoroutine(TextScaleCoroutine(waveText, waveTextTargettScale, waveTextStartScale));
    }
    #endregion

    #region Line Move
    IEnumerator LineMoveCoroutine(RectTransform rect, Vector2 targetPosition, Vector2 startPosition)
    {
        yield return StartCoroutine(UIMoveCoroutine(rect, targetPosition));
        yield return waitStayTime;
        yield return StartCoroutine(UIMoveCoroutine(rect, startPosition));
    }

    IEnumerator UIMoveCoroutine(RectTransform rect, Vector2 position)
    {
        float t = 0f;
        Vector2 localPosition = rect.localPosition;
        while (t < 1f)
        {
            t += Time.deltaTime/ animationTime;
            rect.localPosition = Vector2.Lerp(localPosition, position, t); 
            yield return null;
        }
    }
    #endregion

    #region Text Scale
    IEnumerator TextScaleCoroutine(RectTransform rect, Vector2 targetScale, Vector2 startScale)
    {
        yield return StartCoroutine(UIScaleCoroutine(rect, targetScale));
        yield return waitStayTime;
        yield return StartCoroutine(UIScaleCoroutine(rect, startScale));
    }

    IEnumerator UIScaleCoroutine(RectTransform rect, Vector2 scale)
    {
        float t = 0f;
        Vector2 localScale = rect.localScale;
        while (t < 1f)
        {
            t += Time.deltaTime / animationTime;
            rect.localScale = Vector2.Lerp(localScale, scale, t);
            yield return null;
        }
    }
    #endregion

}
