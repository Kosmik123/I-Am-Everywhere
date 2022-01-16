using System.Collections.Generic;
using UnityEngine;

namespace Shadow
{
    [RequireComponent(typeof(SurfaceTouch))]
    public class MeshBend : MonoBehaviour
    {
        const float DELTA = 0.001f;
        readonly int[] triangles = {
            0, 2, 1,
            1, 2, 3,
            2, 4, 3,
            3, 4, 5
        };

        private SurfaceTouch player;

        [Header("To Link")]
        public MeshFilter meshFilter;
        public BendedTransform[] bendedObjects;

        [Header("States")]
        public float bendPoint;

        private Vector3[] verts;
        private Vector2[] uvs;

        private Mesh mesh;

        private void Awake()
        {
            player = GetComponent<SurfaceTouch>();
        }

        private void Start()
        {
            mesh = meshFilter.mesh;
            bendPoint = 1;

            CreateShape();
            RefreshMesh();
        }

        private void LateUpdate()
        {
            if (player.wallTouching < DELTA)
                bendPoint = 0.99f;
            else if (player.floorTouching < DELTA)
                bendPoint = -0.99f;
            else
                bendPoint = player.floorTouching - player.wallTouching;

            CreateShape();
            RefreshMesh();

            foreach (var obj in bendedObjects)
            {
                obj.BendPosition(bendPoint * player.radius);
            }
        }


        private void CreateShape()
        {
            verts = new Vector3[]
            {
                new Vector3(-0.5f, bendPoint < 0 ? bendPoint * 0.5f : 0, bendPoint < 0 ? -(1 + bendPoint) * 0.5f : -0.5f),
                new Vector3(+0.5f, bendPoint < 0 ? bendPoint * 0.5f : 0, bendPoint < 0 ? -(1 + bendPoint) * 0.5f : -0.5f),
                new Vector3(-0.5f, bendPoint < 0 ? bendPoint * 0.5f : 0, bendPoint > 0 ? bendPoint * 0.5f : 0),
                new Vector3(+0.5f, bendPoint < 0 ? bendPoint * 0.5f : 0, bendPoint > 0 ? bendPoint * 0.5f : 0),
                new Vector3(-0.5f, bendPoint > 0 ? (1 - bendPoint) * 0.5f : 0.5f, bendPoint > 0 ? bendPoint * 0.5f : 0),
                new Vector3(+0.5f, bendPoint > 0 ? (1 - bendPoint) * 0.5f : 0.5f, bendPoint > 0 ? bendPoint * 0.5f : 0)
            };

            uvs = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 0.5f + bendPoint * 0.5f),
                new Vector2(1, 0.5f + bendPoint * 0.5f),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };


        }

        private void RefreshMesh()
        {
            mesh.Clear();
            mesh.vertices = verts;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            foreach (var obj in bendedObjects)
                obj.Validate();
        }

        private void OnDrawGizmos()
        {
            if (player != null)
            {
                Vector3 borderPoint = new Vector3(player.radius, 0);
                Vector3 bendPosition = transform.position + player.radius * bendPoint * Vector3.forward;
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(bendPosition - borderPoint, bendPosition + borderPoint);
            }
        }
#endif


    }
}