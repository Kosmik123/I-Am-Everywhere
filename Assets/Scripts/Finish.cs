using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static event Action OnVictory;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            OnVictory?.Invoke();
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.CompareTag("Player"))
        {
            OnVictory?.Invoke();
        }
    }
}
