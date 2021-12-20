using UnityEngine;

[System.Serializable]
public class BendedTransform
{
    [SerializeField]
    private Transform transform;
    public Vector2 position2d;

    public void BendPosition(float bendDegree)
    {
        transform.localPosition = CalculatePoint(position2d, bendDegree);
    }


    public static Vector3 CalculatePoint(Vector2 point, float bendDegree)
    {
        /*if (bend < -ShadowPlayer.TOUCH_DISTANCE)
        {
            return new Vector3(point.x, point.y);
        }
        else if (bend > ShadowPlayer.TOUCH_DISTANCE)
        {
            return new Vector3(point.x, 0, point.y);
        }
        else*/
        {
            float x = point.x;
            float y = Mathf.Max(0, point.y - bendDegree);
            float z = Mathf.Min(0, point.y - bendDegree);

            if (bendDegree > 0)
                z += bendDegree;
            else
                y += bendDegree;

            return new Vector3(x, y, z);
        }
    }

    public void Validate()
    {
        transform.localPosition = CalculatePoint(position2d, -1);
    }

}