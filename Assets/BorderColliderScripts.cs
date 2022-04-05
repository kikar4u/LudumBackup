using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderColliderScripts : MonoBehaviour
{
    public EdgeCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider.SetPoints(new List<Vector2>());
        CreateBorder();
    }
    
    void CreateBorder()
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 Objpos =  transform.GetChild(i).transform.position;

            points.Add(new Vector2(Objpos.x, Objpos.z));
        }

        //Vector2[] pos = points.ToArray();
        collider.SetPoints(points);
    }
}
