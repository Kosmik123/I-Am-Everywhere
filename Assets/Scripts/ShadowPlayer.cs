using UnityEngine;

public class ShadowPlayer : MonoBehaviour
{
    public const float TOUCH_DISTANCE = 0.9f;

    [Header("Properties")]
    public float radius;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private LayerMask floorLayer;

    [Header("States")]
    public float wallTouching;
	public float floorTouching;

	private void Awake()
	{
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, radius, floorLayer))
        {
            transform.position = hitInfo.point + Vector3.up * 0.001f;
        }
	}

	private void Update()
    {
        floorTouching = CheckTouching(Vector3.down, floorLayer);
        wallTouching = CheckTouching(Vector3.forward, wallLayer);
    }

    private float CheckTouching(Vector3 direction, LayerMask layer)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, radius, layer))
            return 1 - hitInfo.distance / radius;
        return 0;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * radius);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * radius);
    }
#endif

}



