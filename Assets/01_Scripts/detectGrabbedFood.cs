using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectGrabbedFood : MonoBehaviour
{
    public LayerMask m_food;
    public List<Food_Behaviours> hitFood;
    public Collider[] hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // hit = Physics.OverlapSphere(transform.position, 2f, m_food);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            hitFood.Add(other.gameObject.GetComponent<Food_Behaviours>());
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            hitFood.Remove(other.gameObject.GetComponent<Food_Behaviours>());

        }
    }
}
