using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("To Link")]
    public Image damagePanel;
    public TMPro.TMP_Text pointsText;

    [Header("Settings")]
    public AnimationCurve damageAnimCurve;

    private float progress = 1;
    
    private void OnEnable()
    {
        progress = 1;
        Health.OnChangeHealth += ShowDamage;
    }


    private void ShowDamage(int points)
    {
        progress = 0;
        pointsText.text = points.ToString();
    }

    private void RefreshPoints(int points)
    {

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
        Health.OnChangeHealth -= ShowDamage;

    }
}
