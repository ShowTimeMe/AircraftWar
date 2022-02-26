using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Text timeScaleText;

    IEnumerator Start()
    {
        slider.value = Time.timeScale;

        yield return null;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        while (isActiveAndEnabled)
        {
            timeScaleText.text = "Time Scale = " + Time.timeScale.ToString("f2");

            yield return null;
        }
    }

    public void OnValueChanged(float value)
    {
        Time.timeScale = slider.value;
    }
}