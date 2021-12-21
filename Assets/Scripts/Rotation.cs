using UnityEngine;

public class Rotation : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public float speed;

    public float startAngle;
    public float endAngle;
    
    public Vector3 axis;

    // states
    private float progress;

    private void Start()
    {
        progress = Random.Range(0f, 1f);
    }

    private void Update()
    {
        UpdateProgress();

        float modifiedProgress = moveCurve.Evaluate(progress);

        float currentAngle = Mathf.Lerp(startAngle, endAngle, modifiedProgress);
        transform.localRotation = Quaternion.AngleAxis(currentAngle, axis);
    }

    private void UpdateProgress()
    {
        progress += Time.deltaTime * speed;
        if (progress >= 1)
            progress = 0;
    }
    
    private void OnValidate()
    {
        Normalize(axis);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + axis * 5);
    }
    
}
