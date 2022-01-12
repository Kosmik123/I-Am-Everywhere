using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("ROOM PARTS")]
    [SerializeField] Transform wall;
    [SerializeField] Transform floor;

    [Header("ROOM SIZE")]
    [SerializeField] float width = 1;
    [SerializeField] float height = 1;
    [SerializeField] float depth = 1;

    private MeshFilter meshFilter;

    public void Generate()
    {
        Vector3 wallScale = wall.localScale;
        wallScale.x = width;
        wallScale.y = height;
        wall.localScale = wallScale;

        Vector3 floorScale = floor.localScale;
        floorScale.x = width;
        floorScale.y = depth;
        floor.localScale = floorScale;

        Vector3 wallPosition = wall.localPosition;
        wallPosition.y = height * 0.5f;
        wallPosition.z = depth * 0.5f;
        wall.localPosition = wallPosition;
    }

    private void OnValidate()
    {
        Generate();
    }
}
