using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShadowBend))]
public class LightDetection : MonoBehaviour
{
    private ShadowBend bend;
    private ShadowPlayer player;

    [Header("Settings")]
    public Vector2[] detectionPoints;
    public LayerMask objectsLayer;

    public Light[] directionalLights;
    public Light[] pointLights;

    [Range(0,1)]
    public float pointLightRangeModifier;

    private float radius;

    [Header("States")]
    private Vector3[] calculatedPoints;
    public bool isInLight;

    [Header("Debug")]
    public bool testLightCheck;
    public bool showLines;

    private void Awake()
    {
        bend = GetComponent<ShadowBend>();
        player = GetComponent<ShadowPlayer>();
        calculatedPoints = new Vector3[detectionPoints.Length];
    }

    void Start()
    {
        radius = player.radius;
    }

    void Update()
    {
        float bendDegree = bend.bendPoint * radius;
        for (int i = 0; i < calculatedPoints.Length; i++)
        {
            Vector2 point = detectionPoints[i];
            calculatedPoints[i] = BendedTransform.CalculatePoint(point, bendDegree);
        }
    }


    private void CheckLights()
    {
        foreach(var point in calculatedPoints)
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
        if (!pointLight.enabled)
            return false;
        if ((lightPos - point).magnitude > pointLight.range * pointLightRangeModifier)
            return false;

        return !Physics.Raycast(point, lightPos - point, 100, objectsLayer);
    }

    private bool IsPointInDirectionalLight(Vector3 point, Light directionalLight)
    {
        if (!directionalLight.enabled)
            return false;
        Quaternion lightRotation = directionalLight.transform.rotation;
        return !Physics.Raycast(point, lightRotation * Vector3.back, 100, objectsLayer);
    }

    public bool IsInLight()
    {
        CheckLights();
        return isInLight;
    }

    private void OnValidate()
    {
        player = GetComponent<ShadowPlayer>();
        for(int i = 0; i < detectionPoints.Length; i++)
        {
            Vector2 point = detectionPoints[i];
            float x = Mathf.Clamp(point.x, -player.radius, player.radius);
            float y = Mathf.Clamp(point.y, -player.radius, player.radius);
            detectionPoints[i] = new Vector2(x, y);
        }


        if(testLightCheck)
        {
            testLightCheck = false;
            CheckLights();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            foreach (var point in calculatedPoints)
                Gizmos.DrawSphere(transform.position + point, 0.05f);

            if (showLines)
            {
                foreach (var point in calculatedPoints)
                {
                    foreach (var light in directionalLights)
                    {
                        bool inLight = IsPointInDirectionalLight(transform.position + point, light);
                        Gizmos.color = inLight ? Color.yellow : new Color(1, 1, 1, 0.2f);
                        Gizmos.DrawLine(light.transform.position, transform.position + point);
                    }

                    foreach (var light in pointLights)
                    {
                        bool inLight = IsPointInPointLight(transform.position + point, light);
                        Gizmos.color = inLight ? Color.yellow : new Color(1, 1, 1, 0.2f);

                        Gizmos.DrawLine(light.transform.position, transform.position + point);
                    }
                }
            }
        }
        else
        {
            foreach (var point in detectionPoints)
                Gizmos.DrawSphere(transform.position + (Vector3)point, 0.05f);

        }
       

    }

}
