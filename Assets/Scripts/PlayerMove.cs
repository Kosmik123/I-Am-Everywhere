using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class ShadowPlayer
{
    [SerializeField]
    private float radius;



}
*/

public class PlayerMove : MonoBehaviour
{
    private new Rigidbody rb;
    private new Collider collider;

    [Header("To Link")]
    public Transform graphic;

    [Header("Properties")]
    public LayerMask wallLayer;
    public LayerMask floorLayer;

    public float radius;
    public float moveSpeed;

    [Header("States")]
    [SerializeField] private float wallTouching;
    [SerializeField] private float floorTouching;

    private float rotationAngle;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        floorTouching = CheckTouching(Vector3.down, floorLayer);
        wallTouching = CheckTouching(Vector3.forward, wallLayer);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        Move(move);
    }

    private float CheckTouching(Vector3 direction, LayerMask layer)
    {
        if(Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, radius, layer))
            return 1 - hitInfo.distance / radius;
        return 0;
    }

    void Move(Vector2 direction)
    {
        Vector3 vertical;
        if(direction.y > 0 && wallTouching > 0.5)
            vertical = Vector3.up * direction.y;
        else if (direction.y < 0 && floorTouching > 0.5) 
            vertical = Vector3.forward * direction.y;
        else
            vertical = (floorTouching > 0.9 ? Vector3.forward : Vector3.up) * direction.y;

        rb.velocity = direction.x * moveSpeed * Vector3.right + vertical;
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



