using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;

    public float rotationFactor;

    private void Start()
    {
        Refresh();
    }



    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        float x = player.position.x;
        float angle = x * rotationFactor;

        float xCam = x + Mathf.Tan(Mathf.Deg2Rad * angle) * (transform.position.z - player.position.z);

        transform.position = new Vector3(xCam, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
    }


}
