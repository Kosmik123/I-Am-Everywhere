using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShadowPlayer))]
public class ShadowBend : MonoBehaviour
{
    const float DELTA = 0.001f;

    private ShadowPlayer player;

    [Header("To Link")]
    public Transform graphic;
    public MeshFilter meshFilter;

    [Header("States")]
    public float bendPoint;

    public List<Vector3> newMeshVerts;

    private Mesh originalMesh;

    private void Awake()
    {
        player = GetComponent<ShadowPlayer>();
    }

    private void Start()
    {
        originalMesh = meshFilter.mesh;
    }

    private void Update()
    {
        if (player.wallTouching < DELTA)
            bendPoint = 1;
        else if(player.floorTouching < DELTA)
            bendPoint = -1;
        else
            bendPoint = player.floorTouching - player.wallTouching; 

        if(bendPoint > -1 && bendPoint < 1)
        {
            newMeshVerts.Clear();
            foreach (var vert in originalMesh.vertices)
                newMeshVerts.Add(vert);

            newMeshVerts.Add(new Vector3(-0.5f, 0.5f * bendPoint));
            newMeshVerts.Add(new Vector3(0.5f, 0.5f * bendPoint));
        }



    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Vector3 borderPoint = new Vector3(player.radius, 0);
            Vector3 bendPosition = transform.position + player.radius * bendPoint * Vector3.forward ;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(bendPosition - borderPoint, bendPosition + borderPoint);
        }
    }
#endif


}



