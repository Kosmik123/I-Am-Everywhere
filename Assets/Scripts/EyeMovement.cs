using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
    public PlayerMove playerMove;
    public ShadowBend shadowBend;

    public float maxExtent;


    private void Update()
    {
        float neutralAngle = shadowBend.bendPoint * 45 - 45;
        transform.localRotation = Quaternion.AngleAxis(neutralAngle, Vector3.right);
        transform.localRotation *= Quaternion.Euler(
            playerMove.move.y * maxExtent, 
            0,
            -playerMove.move.x * maxExtent);
    }

}
