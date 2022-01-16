using UnityEngine;

namespace Shadow
{
    public static class Bend
    {
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

    }
}