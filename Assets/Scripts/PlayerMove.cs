using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private new Rigidbody rb;
    private new Collider collider;

    [Header("To Link")]
    public Transform graphic;

    [Header("Properties")]
    public LayerMask wallLayer;
    public LayerMask floorLayer;

    public float touchDistance;
    public float moveSpeed;

    [Header("States")]
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isTouchingFloor;

    private float rotationAngle;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        CheckWall();
        CheckFloor();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        Move(move);
    }

    private void CheckWall()
    {
        isTouchingWall = Physics.Raycast(transform.position, Vector3.forward, touchDistance, wallLayer);
    }

    private void CheckFloor()
    {
        isTouchingFloor = Physics.Raycast(transform.position, Vector3.down, touchDistance, floorLayer);
    }

    void Move(Vector2 direction)
    {
        Vector3 vertical;
        if(direction.y > 0 && isTouchingWall)
            vertical = Vector3.up * direction.y;
        else if (direction.y < 0 && isTouchingFloor) 
            vertical = Vector3.forward * direction.y;
        else
            vertical = (isTouchingFloor ? Vector3.forward : Vector3.up) * direction.y;

        rb.velocity = Vector3.right * direction.x * moveSpeed + vertical;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * touchDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * touchDistance);
    }



#endif
}



