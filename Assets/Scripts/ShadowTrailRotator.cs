using UnityEngine;

public class ShadowTrailRotator : MonoBehaviour
{
	[SerializeField]
	private ShadowPlayer shadowPlayer;

	[SerializeField]
	private float currentAngle;

	private void Update()
	{
		currentAngle = Mathf.Rad2Deg * Mathf.Atan(shadowPlayer.floorTouching / shadowPlayer.wallTouching);
		if (float.IsNaN(currentAngle) == false)
			transform.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.right);
	}
}
