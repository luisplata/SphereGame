using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    [SerializeField] private InputFacade input;
    [SerializeField] private LineRenderer lineRendererToPull, lineRendererToGo;
    public Vector2 direction1, direction2;

    private void Update()
    {
        var pointA = input.FirstPosition;
        var pointB = input.SecondPosition;
        if(pointA == Vector2.zero && pointB == Vector2.zero) return;
        // Update the line renderer positions
        lineRendererToPull.SetPosition(0, pointA);
        lineRendererToPull.SetPosition(1, pointB);
        //render to go the sphere
        lineRendererToGo.SetPosition(0, pointA);
        lineRendererToGo.SetPosition(1, CalculateFinalPosition(pointA, (pointA - pointB)));
    }

    
    private Vector2 CalculateFinalPosition(Vector2 initialPos, Vector2 vel)
    {
        return initialPos + (vel * 1f);
    }
}
