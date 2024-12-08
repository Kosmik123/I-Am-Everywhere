using UnityEngine;

[RequireComponent(typeof(ShadowBend))]
public class LightDetection : MonoBehaviour
{
    private ShadowBend bend;
    private ShadowPlayer player;

    [Header("Settings")]
    public Vector2[] detectionPoints;
    public LayerMask objectsLayer;

    [SerializeField]
    private Light[] directionalLights;
    [SerializeField]
    private Light[] pointLights;
    [SerializeField]
    private Light[] spotLights;

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

            foreach (var light in spotLights)
            {
				if (IsPointInSpotLight(transform.position + point, light))
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

        var direction = lightPos - point;
		return !Physics.Raycast(point,  direction, 100, objectsLayer) && !Physics.Raycast(lightPos, -direction, 100, objectsLayer);
    }

    private bool IsPointInDirectionalLight(Vector3 point, Light directionalLight)
    {
        if (!directionalLight.enabled)
            return false;
        Quaternion lightRotation = directionalLight.transform.rotation;
        return !Physics.Raycast(point, lightRotation * Vector3.back, 100, objectsLayer);
    }

    private bool IsPointInSpotLight(Vector3 point, Light spotLight)
    {
        var receivedLightAmount = Lumi.SampleSpotLight(spotLight, point, objectsLayer);
        return receivedLightAmount > 0.01f;
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


public static class Lumi
{
	private const float RedCoef = 0.2989f;
	private const float GreenCoef = 0.5870f;
	private const float BlueCoef = 0.1140f;

    public static float SamplePointLight(Light light, Vector3 samplePoint, LayerMask lightRaycastMask, bool perceivedBrightness = false)
	{
		Transform lightTransform = light.transform;
		Vector3 rayDirectionMag = samplePoint - lightTransform.position;
		float lightDistance = rayDirectionMag.magnitude;
		if (lightDistance > light.range)
		{
			return 0;
		}

		if (light.shadows != LightShadows.None)
		{
			Vector3 rayDirectionNorm = rayDirectionMag.normalized;
			if (Physics.Raycast(lightTransform.position, rayDirectionNorm, lightDistance, lightRaycastMask))
			{
				return 0;
			}
		}

		float inverseSquareRange = 1f / Mathf.Max(light.range * light.range, 0.00001f);

		float distanceSqr = Mathf.Max(rayDirectionMag.sqrMagnitude, 0.00001f);
		float rangeAttenuation = Mathf.Sqrt(Mathf.Min(1.0f - Mathf.Sqrt(distanceSqr * inverseSquareRange), 1));
		float attenuation = rangeAttenuation / distanceSqr;

		attenuation = Mathf.Min(1, attenuation);

		float lightIntensity = light.intensity;
		if (perceivedBrightness)
		{
			float adjustedColorInt = (light.color.r * RedCoef) + (light.color.g * GreenCoef) + (light.color.b * BlueCoef);
			lightIntensity *= adjustedColorInt;
		}

		return attenuation * lightIntensity;
	}

	public static float SampleSpotLight(Light light, Vector3 samplePoint, LayerMask lightRaycastMask, bool perceivedBrightness = false)
	{
		Transform lightTransform = light.transform;
		Vector3 rayDirectionMag = samplePoint - lightTransform.position;
		float lightDistance = rayDirectionMag.magnitude;
		if (lightDistance > light.range)
		{
			return 0;
		}

		Vector3 rayDirectionNorm = rayDirectionMag.normalized;
		float outerCos = Mathf.Cos(Mathf.Deg2Rad * 0.5f * light.spotAngle);
		float forwardDotRay = Vector3.Dot(lightTransform.forward, rayDirectionNorm);
		if (forwardDotRay < outerCos)
		{
			return 0;
		}

		if (light.shadows != LightShadows.None)
		{
			if (Physics.Raycast(lightTransform.position, rayDirectionNorm, lightDistance, lightRaycastMask))
			{
				return 0;
			}
		}

		float inverseSquareRange = 1f / Mathf.Max(light.range * light.range, 0.00001f);

		float distanceSqr = Mathf.Max(rayDirectionMag.sqrMagnitude, 0.00001f);
		float rangeAttenuation = Mathf.Sqrt(Mathf.Min(1.0f - Mathf.Sqrt(distanceSqr * inverseSquareRange), 1));

		float innerCos = Mathf.Cos(Mathf.Deg2Rad * 0.5f * light.innerSpotAngle);
		float angleRangeInv = 1f / Mathf.Max(innerCos - outerCos, 0.001f);
		float spotAnglesInner = angleRangeInv;
		float spotAnglesOuter = -outerCos * angleRangeInv;

		float spotAttenuation = Mathf.Sqrt(Mathf.Clamp01(forwardDotRay * spotAnglesInner + spotAnglesOuter));
		float attenuation = spotAttenuation * (rangeAttenuation / distanceSqr);

		attenuation = Mathf.Min(1, attenuation);

		float lightIntensity = light.intensity;
		if (perceivedBrightness)
		{
			float adjustedColorInt =
				(light.color.r * RedCoef) + (light.color.g * GreenCoef) + (light.color.b * BlueCoef);
			lightIntensity = lightIntensity * adjustedColorInt;
		}

		return attenuation * lightIntensity;
	}

}