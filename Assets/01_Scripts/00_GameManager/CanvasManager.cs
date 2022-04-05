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
    public GameObject m_GamePanel;
    public GameObject m_PausePanel;

    [Header("Slider")]
    public Slider m_StarvingGourmetSlider;
    public Slider m_IndegestionGourmetSlider;

    [Header("Score")]
    public TMPro.TMP_Text ScoreText;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CanvasManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpGourmetSlider();
        UpdateScoreText(0);
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

    public void UpdateScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void ShowPausePanel()
    {
        m_PausePanel.SetActive(true);
        m_GamePanel.SetActive(false);
    }

    public void HidePausePanel()
    {
        GameManager.instance.Status = GameStatus.RUNNING;
        Time.timeScale = 1;
        m_PausePanel.SetActive(false);
        m_GamePanel.SetActive(true);
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
