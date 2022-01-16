using UnityEngine;

namespace Shadow
{
    [System.Serializable]
    public class BendedTransform
    {
        [SerializeField]
        private Transform transform;
        public Vector2 position2d;

        public void BendPosition(float bendDegree)
        {
            transform.localPosition = Bend.CalculatePoint(position2d, bendDegree);
        }


        
        public void Validate()
        {
            transform.localPosition = Bend.CalculatePoint(position2d, 1);
        }

    }
}