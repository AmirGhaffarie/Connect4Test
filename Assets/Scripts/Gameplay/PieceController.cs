using System.Collections;
using System.Linq;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField] AnimationCurve toColumnCurve;
    [SerializeField] AnimationCurve dropCurve;

    public void MoveToPosition(Vector3 columnPos, Vector3 endPos)
    {
        StartCoroutine(MovePieceRoutine(columnPos, endPos));
    }

    private IEnumerator MovePieceRoutine(Vector3 columnPos, Vector3 endPos)
    {
        yield return MoveWithCurve(toColumnCurve, transform.position, columnPos);
        yield return MoveWithCurve(dropCurve, transform.position, endPos);
    }

    IEnumerator MoveWithCurve(AnimationCurve curve, Vector3 startPos , Vector3 endPos)
    {
        var transform = this.transform;
        var time = curve.keys.First().time;
        var endTime = curve.keys.Last().time;
        while (time < endTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            yield return null;
        }
        transform.position = endPos;
    }
}
