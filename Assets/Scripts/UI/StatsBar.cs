using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 
/// </summary>
public class StatsBar : MonoBehaviour
{
    Canvas canvas;

    [SerializeField] Image fillImageBack;
    [SerializeField] Image fillImageFront;
    [SerializeField] bool delayFill = true;
    [SerializeField] float fillDelay = 10f;//填充延迟时间
    [SerializeField] float fillSpeed = 0.1f;//状态条填充速度

    float currentFillAmount;
    protected float targetFillAmount;

    float PreviousFillAmount;

    float t;

    WaitForSeconds waitForDelayFill;
    Coroutine bufferedFillingCoroutine;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        waitForDelayFill = new WaitForSeconds(fillDelay);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// 能量血条初始化函数
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    public virtual  void InitIalize(float currentValue,float maxValue)
    {
        currentFillAmount = currentValue / maxValue;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }


    /// <summary>
    /// 更新状态条
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    public virtual void UpdateStats(float currentValue, float maxValue)
    {
        targetFillAmount = currentValue / maxValue;

        if (bufferedFillingCoroutine != null)
        {
            StopCoroutine(bufferedFillingCoroutine);
        }
        //前面图片的填充值=目标填充值------慢慢减少后面图片的填充值
        if (currentFillAmount > targetFillAmount)
        {
            fillImageFront.fillAmount = targetFillAmount;

            bufferedFillingCoroutine=StartCoroutine(BufferedFillingCoroutine(fillImageBack));
        }
        else if (currentFillAmount < targetFillAmount)//后面图片的填充值=目标填充值------慢慢增加前面图片的填充值
        {
            fillImageBack.fillAmount = targetFillAmount;
            bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageFront));
        }

    }

    /// <summary>
    /// 缓冲填充协程
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    protected virtual IEnumerator BufferedFillingCoroutine(Image image)
    {
        if (delayFill)
        {
            yield return 0.5f;
        }
        PreviousFillAmount = currentFillAmount;
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * fillSpeed;
            currentFillAmount = Mathf.Lerp(PreviousFillAmount, targetFillAmount, t);
            image.fillAmount = currentFillAmount;
            yield return null;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
