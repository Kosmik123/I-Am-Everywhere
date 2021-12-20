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

    private float radius;

    [Header("States")]
    private Vector3[] calculatedPoints;

    public bool isInLight;

    public bool testLightCheck;

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
        float bendPoint = bend.bendPoint;
        for (int i = 0; i < calculatedPoints.Length; i++)
        {
            Vector2 point = detectionPoints[i];
            if (bendPoint < -ShadowPlayer.TOUCH_DISTANCE)
            {
                calculatedPoints[i] = new Vector3(point.x, point.y);
            }
            else if (bendPoint > ShadowPlayer.TOUCH_DISTANCE)
            {
                calculatedPoints[i] = new Vector3(point.x, 0, point.y);
            }
            else
            {
                float bendPos = bendPoint * radius;
                float x = calculatedPoints[i].x;
                float y = Mathf.Max(0, point.y - bendPos);
                float z = Mathf.Min(0, point.y - bendPos);

                if (bendPoint > 0)
                    z += bendPos;
                else
                    y += bendPos;

                calculatedPoints[i] = new Vector3(x, y, z);
            }
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
        return !Physics.Raycast(point, lightPos - point, 100, objectsLayer);
    }

    private bool IsPointInDirectionalLight(Vector3 point, Light directionalLight)
    {
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
            foreach (var point in calculatedPoints)
                Gizmos.DrawSphere(transform.position + point, 0.05f);
        else
            foreach (var point in detectionPoints)
                Gizmos.DrawSphere(transform.position + (Vector3)point, 0.05f);

    }

}
