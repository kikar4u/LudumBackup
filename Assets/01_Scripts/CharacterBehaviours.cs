using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviours : MonoBehaviour,IDamageable<float>,IKillable
{
    [Header("Param")]
    public float m_Life;

    [Header("Timer")]
    [Min(0.2f)]
    public float m_dontMoveTime;
    private Timer t_BurnCookTimer;

    [Space]
    public CharacterController controller;
    public CharacterFeedbacks feedback;
    public bool hasObject = false;

    public Animator a_PlayerAnimator;

    private void Start()
    {
        t_BurnCookTimer = new Timer(m_dontMoveTime, GetBurn);
        feedback.ChangeIndicator((int)m_Life);
    }


    // Update is called once per frame
    void Update()
    {
        if (controller.velocity != Vector3.zero)
        {
            t_BurnCookTimer.Pause();
            t_BurnCookTimer.Reset();
            feedback.StopBurnAnim();

            a_PlayerAnimator.SetBool("Run", true);

            return;
        }

        a_PlayerAnimator.SetBool("Run", false);

        if (t_BurnCookTimer.IsStarted())
            return;

        t_BurnCookTimer.ResetPlay();
    }


    private void GetBurn()
    {
        //print("j'ai chaud");
        TakeDamage(1);
        feedback.PlayBurnFeedback();
    }

    public bool IsDead()
    {
        return m_Life <= 0;
    }

    public void Dead()
    {
        GameManager.instance.GameOver();
        //print("dead");
    }

    public void TakeDamage(float damage)
    {
        m_Life -= damage;
        feedback.ChangeIndicator((int)m_Life);

        if (IsDead())
            Dead();
    }
}
