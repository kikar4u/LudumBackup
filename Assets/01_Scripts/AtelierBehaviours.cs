using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierBehaviours : MonoBehaviour
{
    public Food_Behaviours[] m_Preparingfoods;


    [Header("Timer")]
    [Min(0.2f)]
    public float m_CookingTime;
    protected Timer t_CookingTimer;
    public GameObject feedBackTimer;

    public FMODUnity.EventReference atelierSound;
    protected FMOD.Studio.EventInstance atelierEvent;

    public FMODUnity.EventReference materialSound;
    protected FMOD.Studio.EventInstance materialEvent;

    // Start is called before the first frame update
    void Start()
    {
        atelierEvent = FMODUnity.RuntimeManager.CreateInstance(atelierSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(atelierEvent, transform);

        materialEvent = FMODUnity.RuntimeManager.CreateInstance(materialSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(materialEvent, transform);
    }

    // Update is called once per frame
    void Update()
    {
/*        Debug action
 *        if (Input.GetKeyDown(KeyCode.Escape))
            DropFood(FindObjectsOfType<Food_Behaviours>());*/
    }

    public virtual void DropFood(Food_Behaviours[] foods)
    {
        m_Preparingfoods = foods;
        t_CookingTimer = new Timer(m_CookingTime * foods.Length, CookFood);
        t_CookingTimer.ResetPlay();
    }

    public virtual void CookFood()
    {
        foreach (var item in m_Preparingfoods)
        {
            print(item.name);
        }

        m_Preparingfoods = null;
    }

    internal void TakeFood()
    {
        t_CookingTimer.Pause();
        atelierEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
