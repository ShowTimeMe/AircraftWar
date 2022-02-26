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
    [SerializeField] float fillDelay = 10f;//����ӳ�ʱ��
    [SerializeField] float fillSpeed = 0.1f;//״̬������ٶ�

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
    /// ����Ѫ����ʼ������
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
    /// ����״̬��
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
        //ǰ��ͼƬ�����ֵ=Ŀ�����ֵ------�������ٺ���ͼƬ�����ֵ
        if (currentFillAmount > targetFillAmount)
        {
            fillImageFront.fillAmount = targetFillAmount;

            bufferedFillingCoroutine=StartCoroutine(BufferedFillingCoroutine(fillImageBack));
        }
        else if (currentFillAmount < targetFillAmount)//����ͼƬ�����ֵ=Ŀ�����ֵ------��������ǰ��ͼƬ�����ֵ
        {
            fillImageBack.fillAmount = targetFillAmount;
            bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageFront));
        }

    }

    /// <summary>
    /// �������Э��
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
