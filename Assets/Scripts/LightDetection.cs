using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [Header("Settings")]
    public Vector3[] detectionPoints;
    public LayerMask objectsLayer;

    public Light[] directionalLights;
    public Light[] pointLights;


    [Header("States")]
    public bool isInLight;

    public bool testLightCheck;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckLights()
    {
        foreach(var point in detectionPoints)
        {
            foreach(var light in directionalLights)
            {
                if (IsPointInDirectionalLight(transform.position + point, light))
                {
                    isInLight = true;
                    return;
                }
            }

            foreach (var light in pointLights)
            {
                if (IsPointInPointLight(transform.position + point, light))
                {
                    isInLight = true;
                    return;
                }
            }


        }
        isInLight = false;
    }


    private bool IsPointInPointLight(Vector3 point, Light pointLight)
    {
        Vector3 lightPos = pointLight.transform.position;
        return !Physics.Raycast(point, lightPos - point, 100, objectsLayer);
    }

    private bool IsPointInDirectionalLight(Vector3 point, Light directionalLight)
    {
        Quaternion lightRotation = directionalLight.transform.rotation;
        return !Physics.Raycast(point, lightRotation * Vector3.back, 100, objectsLayer);
    }




    private void OnValidate()
    {
        if(testLightCheck)
        {
            testLightCheck = false;
            CheckLights();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var point in detectionPoints)
        {
            Gizmos.DrawSphere(transform.position + point, 0.1f);
        }

    }

}
