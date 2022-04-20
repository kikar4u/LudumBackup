using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootStep : MonoBehaviour
{
    public FMODUnity.EventReference footStepSound;
    FMOD.Studio.EventInstance footStepEvent;

    private void Start()
    {
        footStepEvent = FMODUnity.RuntimeManager.CreateInstance(footStepSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footStepEvent, transform);
    }
    public void PlayFootStepSound()
    {
        footStepEvent.start();
    }
}
