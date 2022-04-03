using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GourmetBehaviours : MonoBehaviour
{
    public static GourmetBehaviours instance;

    [Header("Param")]
    public float m_MaxStarvingPoint;
    public float m_StarvingPoint;
    public float m_ReduceStarvingPoint;

    public bool m_IsIndigestion = false;

    public TMPro.TMP_Text StarvingText;

    [Header("Timer")]
    [Min(0.2f)]
    public float m_StravingTime;
    private Timer t_StravingTimer;
    public float m_IndigestionTime;
    private Timer t_IndegestionTimer;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GourmetBehaviours");
        else
            instance = this;
    }

    private void Start()
    {
        t_StravingTimer = new Timer(m_StravingTime, Starving);
        t_IndegestionTimer = new Timer(m_IndigestionTime, StopIndegestion);

        t_StravingTimer.ResetPlay();

        UpdateStarvingText();
    }

    private void Starving()
    {
        m_StarvingPoint -= m_ReduceStarvingPoint;
        UpdateStarvingText();

        if (m_StarvingPoint <= 0)
            print("Game Over");

        t_StravingTimer.ResetPlay();
    }

    public void IncreaseStarvingPoint(float point)
    {
        m_StarvingPoint += point;

        GameManager.instance.UpdateStarvingUI(m_StarvingPoint);

        if (IsIndigestion())
        {
            GameManager.instance.StartIndegestionUI(m_IndigestionTime);
            m_StarvingPoint = m_MaxStarvingPoint;
            m_IsIndigestion = true;
            t_IndegestionTimer.ResetPlay();
            GetComponent<Renderer>().material.color = Color.green;
        }

        t_StravingTimer.ResetPlay();
    }

    public bool IsIndigestion()
    {
        return m_StarvingPoint > m_MaxStarvingPoint;
    }

    public void StopIndegestion()
    {
        m_IsIndigestion = false;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Food_Behaviours food;

        if (!collision.gameObject.TryGetComponent<Food_Behaviours>(out food) || m_IsIndigestion)
            return;

        IncreaseStarvingPoint(food.param.GetPoint(food.m_Level));

        UpdateStarvingText();
    }



    private void UpdateStarvingText()
    {
        StarvingText.text = m_StarvingPoint.ToString();
    }
}
