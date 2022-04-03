using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [Header("Panel")]
    public GameObject m_GourmetPanel;

    [Header("Slider")]
    public Slider m_StarvingGourmetSlider;
    public Slider m_IndegestionGourmetSlider;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpGourmetSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SetUpGourmetSlider()
    {
        m_StarvingGourmetSlider.value = GourmetBehaviours.instance.m_StarvingPoint;
        m_StarvingGourmetSlider.maxValue = GourmetBehaviours.instance.m_MaxStarvingPoint;

        m_IndegestionGourmetSlider.value = 0;
        m_IndegestionGourmetSlider.gameObject.SetActive(false);
    }

    public void UpdateIndegestionSlider(float indigestionTime)
    {
        m_IndegestionGourmetSlider.gameObject.SetActive(true);
        StartCoroutine(LerpSliderValueREDUCE(m_IndegestionGourmetSlider, 0, indigestionTime));
    }

    public void UpdateStarvingSlider(float value)
    {
        StartCoroutine(LerpSliderValueADD(m_StarvingGourmetSlider, value, 0.2f));
    }

    IEnumerator LerpSliderValueADD(Slider slider, float endValue, float duration)
    {
        float time = 0;
        float startValue = slider.value;
        while (time < duration)
        {
            slider.value = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        slider.value = endValue;
    }

    IEnumerator LerpSliderValueREDUCE(Slider slider, float endValue, float duration)
    {
        float time = 0;
        float startValue = slider.value;
        while (time < duration)
        {
            slider.value = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        slider.value = endValue;
    }
}
