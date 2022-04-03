using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierBehaviours : MonoBehaviour
{
    public Food_Behaviours[] m_Preparingfoods;


    [Header("Timer")]
    [Min(0.2f)]
    public float m_CookingTime;
    private Timer t_CookingTimer;

    public FMODUnity.EventReference atelierSound;
    FMOD.Studio.EventInstance atelierEvent;

    // Start is called before the first frame update
    void Start()
    {
        atelierEvent = FMODUnity.RuntimeManager.CreateInstance(atelierSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(atelierEvent, transform);
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
}
