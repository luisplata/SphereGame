using System;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour, ILineRendererController
{
    [SerializeField] private InputFacade input;
    [SerializeField] private LineRenderer lineRendererToPull, lineRendererToGo;
    public Vector2 direction1, direction2;
    private Dictionary<string, LineRenderer> lines = new();

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<ILineRendererController>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<ILineRendererController>();
    }

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

    public void ResetLine()
    {
        lineRendererToPull.SetPosition(0, Vector2.zero);
        lineRendererToPull.SetPosition(1, Vector2.zero);
        lineRendererToGo.SetPosition(0, Vector2.zero);
        lineRendererToGo.SetPosition(1, Vector2.zero);
    }

    public void CreateLine(int layer)
    {
        //create a new gameobject and add a line renderer component
        var line = new GameObject($"Line_{layer}").AddComponent<LineRenderer>();
        //set the line renderer properties
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = 0;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.sortingOrder = layer;
        line.transform.SetParent(transform);
        lines.Add($"Line_{layer}", line);
    }

    public void SetPositionToLastPoint(int layer, Vector3 position)
    {
        if (!lines.ContainsKey($"Line_{layer}")) return;
        var line = lines[$"Line_{layer}"];
        line.SetPosition(line.positionCount - 1, position);
    }

    public void AddPoint(int layer, Vector3 position)
    {
        if (!lines.ContainsKey($"Line_{layer}")) return;
        var line = lines[$"Line_{layer}"];
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);
    }

    public void ResetLines(int currentLayer)
    {
        foreach (var line in lines)
        {
            Destroy(line.Value.gameObject);
        }
        lines.Clear();
        
        if (currentLayer == 0)
        {
            CreateLine(currentLayer);
        }
    }
}

public interface ILineRendererController
{
    void CreateLine(int layer);
    void SetPositionToLastPoint(int layer, Vector3 point);
    void AddPoint(int layer, Vector3 position);
    void ResetLines(int currentLayer);
}
