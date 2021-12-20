using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    private Light light;

    public AnimationCurve curve;
    public float speed;
    public float disableThreshold;
    public float baseIntensity;

    private float progress;

    private void Awake()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        progress += speed * Time.deltaTime;
        if (progress > 1)
            progress = 0;

        float intensity = curve.Evaluate(progress);

        light.intensity = intensity * baseIntensity;
        light.enabled = intensity > disableThreshold;
    }


}


