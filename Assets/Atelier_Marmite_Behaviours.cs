using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atelier_Marmite_Behaviours : AtelierBehaviours
{
    GameObject feedback;
    public override void DropFood(Food_Behaviours[] foods)
    {
        m_Preparingfoods = foods;

        t_CookingTimer = new Timer(m_CookingTime * foods.Length, CookFood);
        if (feedback != null)
        {
            Destroy(feedback);
        }
        feedback = Instantiate(feedBackTimer, gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        t_CookingTimer.ResetPlay();

        materialEvent.start();

        print("ok");
    }
    public override void CookFood()
    {
        foreach (Food_Behaviours item in m_Preparingfoods)
        {
            item.foodStatus = Status.BOILED;
        }
        feedback.GetComponent<SpriteRenderer>().color = Color.blue;
        atelierEvent.start();
    }
}
