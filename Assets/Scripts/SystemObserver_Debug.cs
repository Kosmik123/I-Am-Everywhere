using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SystemObserver_Debug : MonoBehaviour
{
    public GameObject debugPanel;
    public Text debugFPSText;

    float debugTimeOld = 0.0f;
    float debugFPScountNum = 0.0f;
    public float debugTime = 0.5f;

    void Start()
    {
        if (debugTime < 0.01f) { debugTime = 0.01f; } else { }
        StartCoroutine(CalculateParameters());
    }

    private IEnumerator CalculateParameters()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (((debugTimeOld + debugTime) < Time.time))
            {
                debugTimeOld = Time.time;

                debugFPScountNum = (int)(1f / Time.unscaledDeltaTime);
                debugFPSText.text = "FPS: " + debugFPScountNum.ToString();
               
            }
            else
            { }
        }
    }
}
