using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodState
{
    FROZEN,
    RAW,
    MEDIUM,
    WELLDONE,
    BURNED
}

public enum Status
{
    NONE,
    FRY,
    CUT,
    BOILED
}

[System.Serializable]
public class FoodParam {
    public FoodState m_FoodLevel;
    public int m_point;
}

[CreateAssetMenu(fileName = "New Food Parametter", menuName = "New Scriptable Object/New FoodParam")]
public class FoodParam_SO : ScriptableObject
{
    public List<FoodParam> param;

    public float FryMultiplicator;
    public float CutMultiplicator;
    public float BoiledMultiplicator;

    public float GetPoint(FoodState state, Status food)
    {
        int baseValue = 0;
        foreach (var item in param)
        {
            if (item.m_FoodLevel == state)
                baseValue = item.m_point;
        }

        switch (food)
        {
            case Status.FRY:
                return baseValue * FryMultiplicator;
                break;
            case Status.CUT:
                return baseValue * CutMultiplicator;
                break;
            case Status.BOILED:
                return baseValue * BoiledMultiplicator;
                break;
            default:
                return baseValue;
                break;
        }

        
    }
}
