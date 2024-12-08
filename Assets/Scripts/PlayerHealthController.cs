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

	private bool isDamaging;

	private void Awake()
	{
		player = GetComponent<Health>();
	}

	private void Update()
	{
		if (lightDetection.IsInLight() && isDamaging == false)
		{
			StartCoroutine(CheckLightCo());
		}
	}

	private IEnumerator CheckLightCo()
	{
		isDamaging = true;
		int damage = 1;
		while (lightDetection.IsInLight())
		{
			player.TakeDamage(damage);
			OnTakeDamage?.Invoke(damage);
			damage++;
			yield return new WaitForSeconds(waitTime);
		}
		isDamaging = false;
	}

	private void OnValidate()
	{
		if (waitTime <= 0)
			waitTime = 0.01f;
	}

	private void OnDisable()
	{
		isDamaging = false;
	}
}

