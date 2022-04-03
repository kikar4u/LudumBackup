using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Polycrime
{
    public class ShootLogics : MonoBehaviour
    {

        public KeyCode m_ShootInput;
        public bool m_CanShoot = true;

        [Space]

        [Min(1f)]
        public float m_ShootForce = 10f;

        [Min(0.5f)]
        public float m_ShootRadius = 0.5f;
        public LayerMask m_FoodLayer;

        [Header("Timer")]
        [Min(0.2f)]
        public float m_ReloadShootTime;
        private Timer t_CanShootTimer;

        public FMODUnity.EventReference shootSound;
        FMOD.Studio.EventInstance shootEvent;

        private void Start()
        {
            t_CanShootTimer = new Timer(m_ReloadShootTime, EnableShoot);

            shootEvent = FMODUnity.RuntimeManager.CreateInstance(shootSound);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(shootEvent, transform);
        }

        private void Update()
        {
            if (Input.GetKeyDown(m_ShootInput) && m_CanShoot)
            {
                if (!t_CanShootTimer.IsStarted())
                    DisableShoot();
                DetectFood();
            }
        }

        public void DetectFood()
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, m_ShootRadius, m_FoodLayer);

            Food_Behaviours food;

            if (hit.Length <= 0)
            {
                if (!t_CanShootTimer.IsStarted())
                {
                    //print("dezqss");
                    t_CanShootTimer.ResetPlay();
                }
                return;
            }


            foreach (var collider in hit)
            {
                if (collider.gameObject.TryGetComponent<Food_Behaviours>(out food))
                {
                    Shoot(food);
                }
            }
            if (!t_CanShootTimer.IsStarted())
            {
                //print("dezqss");
                t_CanShootTimer.ResetPlay();
            }

        }

        public void Shoot(Food_Behaviours food)
        {
            // print(food.name + " name");
            RaycastHit hit;
            //Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(Vector3.forward) * 10000f, Color.yellow, 1000f);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "Gourmet")
                {
                    Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(Vector3.forward) * 10000f, Color.red, 1000f);
                    GetComponent<PropulsionPad>().HandleTrigger(food.gameObject, food.gameObject.GetComponent<SphereCollider>().bounds);
                    Debug.Log("touchey");
                }

            }
            else
            {
                Debug.Log("no touchey");
                //food.MoveToTheGourmet(GourmetBehaviours.instance.transform.position);
                food.Shoot(transform.parent.position, m_ShootForce);
            }
            shootEvent.start();
        }


        private void DisableShoot()
        {
            m_CanShoot = false;
        }

        private void EnableShoot()
        {
            m_CanShoot = true;
        }

        #region Gizmo

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, m_ShootRadius);
        }

        #endregion
    }
}