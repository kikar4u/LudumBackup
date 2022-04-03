using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    [SerializeField]
    private Transform Destination;
    public LayerMask m_FoodLayer;
    public KeyCode m_ShootInput;
    private bool isOnHead = false;

    public void Update()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var objectPosition = this.transform.position;
        Collider[] hit = Physics.OverlapSphere(transform.position, 2f, m_FoodLayer);
        /*
                if (objectPosition.x - playerPosition.x <= 3 && objectPosition.x - playerPosition.x >= -3
                    && objectPosition.z - playerPosition.z <= 3 && objectPosition.z - playerPosition.z >= -3)
                {*//*        }*/
        if (Input.GetKey(m_ShootInput))
        {
            Destination = GameObject.FindGameObjectWithTag("Destination").transform;
            GetComponent<SphereCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            this.transform.parent = GameObject.Find("Destination").transform;
            this.transform.position = Destination.position;
            isOnHead = true;
        }
        if (Input.GetKey(m_ShootInput) && isOnHead)
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<SphereCollider>().isTrigger = false;
            //GetComponent<Rigidbody>().AddForce(Destination.parent.forward * 2f, ForceMode.Impulse);
            isOnHead = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 2f);
    }
    private void OnMouseUp()
    {


    }
}

