using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    [SerializeField]
    private Transform Destination;
    public LayerMask m_player;
    public KeyCode m_ShootInput;
    public float radiusSize;
    private bool isOnHead = false;
    public Collider[] hit;

    public FMODUnity.EventReference grabSound;
    FMOD.Studio.EventInstance grabEvent;

    private void Start()
    {
        grabEvent = FMODUnity.RuntimeManager.CreateInstance(grabSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(grabEvent, transform);
    }

    public void Update()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var objectPosition = this.transform.position;
        hit = Physics.OverlapSphere(transform.position, radiusSize, m_player);
        /*
                if (objectPosition.x - playerPosition.x <= 3 && objectPosition.x - playerPosition.x >= -3
                    && objectPosition.z - playerPosition.z <= 3 && objectPosition.z - playerPosition.z >= -3)
                {*//*        }*/
        if (Input.GetKeyDown(m_ShootInput) && hit.Length >= 1 && !isOnHead)
        {
            Debug.Log(hit[0].gameObject);

            grabEvent.start();
            GetComponent<Food_Behaviours>().T_FastCookTimer.Pause();
            Destination = GameObject.FindGameObjectWithTag("Destination").transform;
            GetComponent<SphereCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            this.transform.parent = GameObject.Find("Destination").transform;
            this.transform.position = Destination.position;
            isOnHead = true;
        }
        else if(Input.GetKeyDown(m_ShootInput) && isOnHead)
        {
            RaycastHit hitInfo;
            Debug.DrawRay(gameObject.transform.position, GameObject.Find("Character").transform.TransformDirection(Vector3.forward) * 2f, Color.red, 1000f);
            if (Physics.Raycast(transform.position, GameObject.Find("Character").transform.TransformDirection(Vector3.forward) * 2f, out hitInfo))
            {
                
                if (hitInfo.transform.tag == "tools")
                {
                    hitInfo.transform.gameObject.GetComponent<AtelierBehaviours>().DropFood(GameObject.Find("Destination").GetComponent<detectGrabbedFood>().hitFood.ToArray());
                    Physics.IgnoreLayerCollision(8, 8);
                    Debug.Log("ici tools et hop");
                    this.transform.parent = null;
                    //GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<SphereCollider>().isTrigger = false;
                    this.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + 1, hitInfo.transform.position.z);
                    //GetComponent<Rigidbody>().AddForce(Destination.parent.forward * 2f, ForceMode.Impulse);
                    isOnHead = false;
                }
                else
                {
                    print("out");
                    hitInfo.transform.gameObject.GetComponent<AtelierBehaviours>().TakeFood();
                    this.transform.parent = null;
                    GetComponent<Rigidbody>().isKinematic = false;
                    Physics.IgnoreLayerCollision(0, 8, false);
                    GetComponent<SphereCollider>().isTrigger = false;
                    //GetComponent<Rigidbody>().AddForce(Destination.parent.forward * 2f, ForceMode.Impulse);
                    isOnHead = false;
                }

            }
            else
            {
                print("out");
                this.transform.parent = null;
                GetComponent<Rigidbody>().isKinematic = false;
                Physics.IgnoreLayerCollision(0, 8, false);
                GetComponent<SphereCollider>().isTrigger = false;
                //GetComponent<Rigidbody>().AddForce(Destination.parent.forward * 2f, ForceMode.Impulse);
                isOnHead = false;
            }

        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radiusSize);
    }
    private void OnMouseUp()
    {


    }
}

