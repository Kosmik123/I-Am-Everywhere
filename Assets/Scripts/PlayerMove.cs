using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShadowPlayer))]
public class PlayerMove : MonoBehaviour
{
    private ShadowPlayer player;

    private new Rigidbody rb;
    private new Collider collider;

    [Header("To Link")]
    public Transform graphic;

    [Header("Properties")]
    public float moveSpeed;

    private float rotationAngle;


    private void Awake()
    {
        player = GetComponent<ShadowPlayer>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        Move(move);
    }

    void Move(Vector2 direction)
    {
        Vector3 vertical;
        if(direction.y > 0 && player.wallTouching > ShadowPlayer.TOUCH_DISTANCE)
            vertical = Vector3.up * direction.y;
        else if (direction.y < 0 && player.floorTouching > ShadowPlayer.TOUCH_DISTANCE) 
            vertical = Vector3.forward * direction.y;
        else
            vertical = (player.wallTouching > ShadowPlayer.TOUCH_DISTANCE ?  Vector3.up : Vector3.forward) * direction.y;

        rb.velocity = direction.x * moveSpeed * Vector3.right + vertical;
    }



}



