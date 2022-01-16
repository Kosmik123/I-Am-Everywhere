using UnityEngine;

namespace Shadow
{
    [RequireComponent(typeof(SurfaceTouch))]
    public class StupidShadowBend : MonoBehaviour
    {
        private SurfaceTouch player;

        public MeshRenderer wallGraphic;
        public MeshRenderer floorGraphic;

        private void Awake()
        {
            player = GetComponent<SurfaceTouch>();
        }

        private void Update()
        {
            floorGraphic.enabled = player.floorTouching > 0;
            wallGraphic.enabled = player.wallTouching > 0;
        }

    }
}