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
    public Image endPanel;
    public TMPro.TMP_Text endText;


    [Header("Settings")]
    public AnimationCurve damageAnimCurve;

    private float progress = 1;
    
    private void OnEnable()
    {
        progress = 1;
        Health.OnChangeHealth += ShowDamage;
        Health.OnDie += ShowGameOver;
    }

    private void ShowGameOver()
    {
        endPanel.enabled = true;
        endPanel.color = Color.white;

        endText.text = "GAME OVER";
        endText.color = Color.black;
    }

    private void ShowVictory()
    {
        endPanel.enabled = true;
        endPanel.color = Color.black;

        endText.text = "VICTORY";
        endText.color = Color.white;
    }


    private void ShowDamage(int points)
    {
        progress = 0;
        pointsText.text = points.ToString();
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
