using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atelier_Planche_Behaviours : AtelierBehaviours
{
    public override void DropFood(Food_Behaviours[] foods)
    {
        foreach (Food_Behaviours item in foods)
        {
            item.isCut = true;
        }
        print("ok");
    }
}
