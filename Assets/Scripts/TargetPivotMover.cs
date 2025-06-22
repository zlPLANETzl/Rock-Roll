using System.Collections;
using UnityEngine;

public class TargetPivotMover : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.1f;
    [SerializeField] private float waitDuration = 0.5f;
    [SerializeField] private float xRange = 30f;

    private Vector3 centerPos;

    private void Start()
    {
        centerPos = transform.position;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return MoveTo(centerPos + Vector3.left * xRange);
            yield return new WaitForSeconds(waitDuration);

            yield return MoveTo(centerPos);
            yield return new WaitForSeconds(waitDuration);

            yield return MoveTo(centerPos + Vector3.right * xRange);
            yield return new WaitForSeconds(waitDuration);

            yield return MoveTo(centerPos);
            yield return new WaitForSeconds(waitDuration);
        }
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        float elapsed = 0f;
        Vector3 start = transform.position;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
}
