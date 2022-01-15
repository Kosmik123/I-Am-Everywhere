using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsIntersects : MonoBehaviour
{
    public GameObject Cube, Sphere;
    Collider collider1, collider2;

    void Start()
    {
        //Check that the first GameObject exists in the Inspector and fetch the Collider
        if (Cube != null)
            collider1 = Cube.GetComponent<Collider>();

        //Check that the second GameObject exists in the Inspector and fetch the Collider
        if (Sphere != null)
            collider2 = Sphere.GetComponent<Collider>();
    }

    void Update()
    {
        //If the first GameObject's Bounds enters the second GameObject's Bounds, output the message
        if (collider1.bounds.Intersects(collider2.bounds))
        {
            Debug.Log("Bounds intersecting");
        }
    }
}