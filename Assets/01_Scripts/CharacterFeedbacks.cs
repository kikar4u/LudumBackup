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

    [Header("Indicator")]
    public List<Color> colors;
    public SpriteRenderer burnIndicator;

    public FMODUnity.EventReference burnSound;
    FMOD.Studio.EventInstance burnEvent;

    public FMODUnity.EventReference footStepSound;
    FMOD.Studio.EventInstance footStepEvent;

    private void Start()
    {
        burnEvent = FMODUnity.RuntimeManager.CreateInstance(burnSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(burnEvent, transform);

        footStepEvent = FMODUnity.RuntimeManager.CreateInstance(footStepSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footStepEvent, transform);
    }

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

    public void PlayFootStepSound()
    {
        footStepEvent.start();
    }

    public void ChangeIndicator(int burnLevel)
    {
        burnEvent.start();
        int burn = Mathf.Clamp(burnLevel - 1, 0, colors.Count-1);
        burnIndicator.color = colors[burn];
    }
}
