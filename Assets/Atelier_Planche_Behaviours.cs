using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atelier_Planche_Behaviours : AtelierBehaviours
{
    public override void DropFood(Food_Behaviours[] foods)
    {
        m_Preparingfoods = foods;
        t_CookingTimer = new Timer(m_CookingTime * foods.Length, CookFood);
        t_CookingTimer.ResetPlay();
        print("ok");
    }
    public override void CookFood()
    {
        foreach (Food_Behaviours item in m_Preparingfoods)
        {
            item.foodStatus = Status.CUT;
        }
        atelierEvent.start();
    }
}
