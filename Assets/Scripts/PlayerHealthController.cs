using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerHealthController : MonoBehaviour
{
    public static event Action<int> OnTakeDamage;

    private Health player;

    [Header("To Link")]
    public LightDetection lightDetection;

    [Header("Settings")]
    public float waitTime;
    public int lightDamage;


    private void Awake()
    {
        player = GetComponent<Health>();
    }

    private void Start()
    {
        StartCoroutine(nameof(CheckLightCo));    
    }


    private IEnumerator CheckLightCo()
    {
        while (true)
        {
            if (lightDetection.IsInLight())
            {
                player.TakeDamage(lightDamage);
                OnTakeDamage?.Invoke(lightDamage);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnValidate()
    {
        if (waitTime <= 0)
            waitTime = 0.01f;
    }

}

