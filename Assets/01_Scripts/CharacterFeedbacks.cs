using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CharacterFeedbacks : MonoBehaviour
{
    public GameObject m_burnFeedback;
    public float m_ScaleTime;
    public float m_UnScaleTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            PlayBurnFeedback();
    }

    public void PlayBurnFeedback()
    {
        m_burnFeedback.transform.DOScale(.5f, m_ScaleTime).SetEase(Ease.Linear);
    }

    internal void StopBurnAnim()
    {
        m_burnFeedback.transform.DOScale(0, m_UnScaleTime).SetEase(Ease.Linear);
    }
}
