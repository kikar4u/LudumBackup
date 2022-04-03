using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Food_Behaviours : MonoBehaviour
{
    [Header("Param")]
    [Min(1)]
    public FoodState m_Level = FoodState.RAW;
    public FoodParam_SO param;
    public Rigidbody m_rb;

    [Min (0.2f)]
    public float m_DetectionRadius = 1f;
    public LayerMask m_PlaqueLayer;

    [Header("Timer")]
    [Min(0.2f)]
    public float m_FastCookTime;
    private Timer t_FastCookTimer;
    [Min(0.2f)]
    public float m_MediumCookTime;
    private Timer t_MediumCookTimer;

    [Header("Sprite Indicator")]
    public SpriteRenderer m_CookIndicator;
    public List<Color> m_CookColor;

    public bool isCut = false;
    public FMODUnity.EventReference foodCookSound;
    FMOD.Studio.EventInstance foodCookEvent;

    public Timer T_FastCookTimer { get => t_FastCookTimer; set => t_FastCookTimer = value; }

    // Start is called before the first frame update
    void Start()
    {
        T_FastCookTimer = new Timer(m_FastCookTime, ChangeCookstyle);
        t_MediumCookTimer = new Timer(m_MediumCookTime, ChangeCookstyle);
        m_CookIndicator.color = m_CookColor[0];

        foodCookEvent = FMODUnity.RuntimeManager.CreateInstance(foodCookSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(foodCookEvent, transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectZone();
    }

    public void DetectZone()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, m_DetectionRadius, m_PlaqueLayer);

        if (hit.Length <= 0)
            return;

        foreach (var collider in hit)
        {
            if (collider.gameObject.CompareTag("Hot"))
            {
                if (T_FastCookTimer.IsStarted())
                    return;

                Cook();
                return;
            }
        }
    }

    internal void Shoot(Vector3 pos, float speed)
    {
        Vector3 directionPos = VectorsMethods.GetDirectionFromAtoB(transform.position, pos).normalized;
        //HandleTrigger();
        m_rb.AddForce(directionPos * speed, ForceMode.Impulse);
    }

    public void MoveToTheGourmet(Vector3 pos)
    {
        transform.DOMove(pos, 2);
    }

    public void Cook()
    {
        print("cook");
        T_FastCookTimer.ResetPlay();
    }

    public void ChangeCookstyle()
    {
        print("+1");
        m_Level++;
        print(m_Level + " " + (int)m_Level);

        foodCookEvent.start();
        if( m_CookColor.Count > (int)m_Level-1)
        {
            m_CookIndicator.color = m_CookColor[(int)m_Level-1];
        }
    }

    #region Gizmo

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_DetectionRadius);
    }

    #endregion
}
