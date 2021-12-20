using UnityEngine;

public class Rotation : MonoBehaviour
{
    public AnimationCurve moveCurve;
    public float speed;

    public float startAngle;
    public float endAngle;

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
        transform.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }

    private void UpdateProgress()
    {
        progress += Time.deltaTime * speed;
        if (progress >= 1)
            progress = 0;
    }
}
