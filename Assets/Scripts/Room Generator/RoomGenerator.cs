using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("ROOM PARTS")]
    [SerializeField] Transform wall;
    [SerializeField] Transform floor;
    [SerializeField] Transform ceilling;
    [SerializeField] Transform leftWall;
    [SerializeField] Transform rightWall;

    [Header("ROOM SIZE")]
    [SerializeField] float width = 1;
    [SerializeField] float height = 1;
    [SerializeField] float depth = 1;

    private MeshFilter meshFilter;

    public void Generate()
    {
        //Room scale
        Vector3 wallScale = wall.localScale;
        wallScale.x = width;
        wallScale.y = height;
        wall.localScale = wallScale;

        wallScale.x = depth;
        leftWall.localScale = wallScale;
        rightWall.localScale = wallScale;

        Vector3 floorScale = floor.localScale;
        floorScale.x = width;
        floorScale.y = depth;
        floor.localScale = floorScale;

        ceilling.localScale = -floorScale;

        //Room position
        Vector3 wallPosition = wall.localPosition;
        wallPosition.z = depth * 0.5f;
        wallPosition.y = height * 0.5f;
        wall.localPosition = wallPosition;

        Vector3 leftWallPosition = leftWall.localPosition;
        leftWallPosition.x = width * -0.5f;
        leftWallPosition.y = height * 0.5f;
        leftWall.localPosition = leftWallPosition;
        
        Vector3 rightWallPosition = rightWall.localPosition;
        rightWallPosition.x = width * 0.5f;
        rightWallPosition.y = height * 0.5f;
        rightWall.localPosition = rightWallPosition;

        Vector3 ceillingPosition = ceilling.localPosition;
        ceillingPosition.y = height;
        ceilling.localPosition = ceillingPosition;
    }

    private void OnValidate()
    {
        Generate();
    }
}
