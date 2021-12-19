using UnityEngine;

[RequireComponent(typeof(ShadowPlayer))]
public class StupidShadowBend : MonoBehaviour
{
    private ShadowPlayer player;

    public MeshRenderer wallGraphic;
    public MeshRenderer floorGraphic;

    private void Awake()
    {
        player = GetComponent<ShadowPlayer>();
    }

    private void Update()
    {
        floorGraphic.enabled = player.floorTouching > 0;
        wallGraphic.enabled = player.wallTouching > 0;
    }

}



