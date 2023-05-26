using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputFacade : MonoBehaviour, IInputFacade{
    [SerializeField] private LevelLogic level;
    public Action<Vector2> onRelease, onFirstPosition;
    private Vector2 point;
    private bool isCliking;
    private Vector2 direction;
    private Vector2 firstPosition, secondPosition;

    public Vector2 FirstPosition => firstPosition;

    public Vector2 SecondPosition => secondPosition;

    public void Click(CallbackContext env)
    {
        isCliking = env.performed;
        if (env.started)
        {
            firstPosition = level.PositionInLevel.position;
            secondPosition = firstPosition;
            onFirstPosition?.Invoke(firstPosition);
        }
        else if (env.canceled)
        {
            secondPosition = CalculatePositionInWord();
            Vector2 direction = GetDirection(firstPosition, secondPosition);
            onRelease?.Invoke(direction);
            secondPosition = Vector2.zero;
            firstPosition = Vector2.zero;
        }
    }

    private Vector2 CalculatePositionInWord()
    {
        Vector3 scenePosition = Camera.main.ScreenToWorldPoint(point);
        Vector2 scenePosition2D = new Vector2(scenePosition.x, scenePosition.y);
        return scenePosition2D;
    }

    public void Point(CallbackContext env){
        point = env.ReadValue<Vector2>();
        if(isCliking){
            secondPosition = CalculatePositionInWord();
        }
    }
    
    private Vector2 GetDirection(Vector2 fromPosition, Vector2 toPosition)
    {
        return (toPosition - fromPosition).normalized;
    }
}