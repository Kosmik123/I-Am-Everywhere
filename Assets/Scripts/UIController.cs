using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("To Link")]
    public Image damagePanel;

    public AnimationCurve damageAnimCurve;


    private float progress = 1;
    
    private void OnEnable()
    {
        progress = 1;
        PlayerHealthController.OnTakeDamage += ShowDamage;
    }


    private void ShowDamage(int obj)
    {
        progress = 0;  
    }


    void Update()
    {
        if(progress < 1)
        {
            progress += Time.deltaTime;
            Color c = damagePanel.color;
            c.a = damageAnimCurve.Evaluate(progress);
            damagePanel.color = c;
        }
    }

    private void OnDisable()
    {
        PlayerHealthController.OnTakeDamage -= ShowDamage;

    }
}
