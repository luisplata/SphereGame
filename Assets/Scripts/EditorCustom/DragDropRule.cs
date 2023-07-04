using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DragDropRule : MonoBehaviour, IRule
{
    [SerializeField] private float snapFloatMove, snapFloatRotate;
    [SerializeField] private Button buttonMove, buttonRotate;
    private IRulesMediator _allRules;
    private bool _isClicked;
    private Vector2 _currentMouse = Vector2.zero;
    private GameObject _currentElement;
    private Vector3 _offset;
    private Vector3 _originalPosition;
    private int _useCase;
    private Camera _main;

    public void Config(IRulesMediator allRules)
    {
        _allRules = allRules;
        buttonMove.onClick.AddListener(() =>
        {
            _useCase = 0;
        });
        buttonRotate.onClick.AddListener(() =>
        {
            _useCase = 1;
        });
        Debug.Log("DragDropRule: Config");
        _main = Camera.main;
    }
    
    public void OnPoint(InputAction.CallbackContext call)
    {
        _currentMouse = call.ReadValue<Vector2>();
        if (!_isClicked || _currentElement == null) return;
        switch (_useCase)
        {
            case 0:
                MoveElement();
                break;
            case 1:
                RotateElement();
                break;
        }
    }

    private void RotateElement()
    {
        var po = _main.ScreenToWorldPoint(_currentMouse);
        var angle = Mathf.Atan2(po.y - _currentElement.transform.position.y, po.x - _currentElement.transform.position.x) * Mathf.Rad2Deg;
        var snapAngle = Mathf.Round(angle / snapFloatRotate) * snapFloatRotate;
        _currentElement.transform.rotation = Quaternion.Euler(0, 0, snapAngle);
    }

    private void MoveElement()
    {
        var po = _main.ScreenToWorldPoint(_currentMouse);
        _currentElement.transform.position = new Vector3(po.x, po.y, 0) + _offset;
        EndMove();
    }

    public void OnClick(InputAction.CallbackContext call)
    {
        if (call.started)
        {
            _isClicked = true;
            var ray = _main.ScreenPointToRay(_currentMouse);
            foreach (var hit2D in Physics2D.RaycastAll(ray.origin, ray.direction, 1000f))
            {
                if (!hit2D.collider.gameObject.TryGetComponent<DragComponent>(out var drag)) continue;
                _currentElement = hit2D.collider.gameObject;
                _originalPosition = _currentElement.transform.position;
                //save offset from mouse to element
                var po = _main.ScreenToWorldPoint(_currentMouse);
                _offset = _currentElement.transform.position - new Vector3(po.x, po.y, 0);
                break;
            }
        }

        if (call.canceled)
        {
            _isClicked = false;
            if (_currentElement != null)
            {
                var ray = Camera.main.ScreenPointToRay(_currentMouse);
                foreach (var hit2D in Physics2D.RaycastAll(ray.origin, ray.direction, 1000f))
                {
                    if (hit2D.collider.gameObject.TryGetComponent<SnapZone>(out var snap))
                    {
                        switch (_useCase)
                        {
                            case 0:
                                EndMove();
                                break;
                        }
                        break;
                    }
                    else
                    {
                        switch (_useCase)
                        {
                            case 0:
                                _currentElement.transform.position = _originalPosition;
                                break;
                        }
                    }
                }
            }
            _currentElement = null;
        }
    }

    private void EndMove()
    {
        var po = _main.ScreenToWorldPoint(_currentMouse);
        var x = Mathf.Round(po.x / snapFloatMove) * snapFloatMove;
        var y = Mathf.Round(po.y / snapFloatMove) * snapFloatMove;
        _currentElement.transform.position = new Vector3(x, y, 0) + _offset;
    }
    /*
        var pointToMouse = Camera.main.ScreenToWorldPoint(_currentMouse);
        var pointToElement = _currentElement.transform.position;
        var angle = Mathf.Atan2(pointToMouse.y - pointToElement.y, pointToMouse.x - pointToElement.x) * Mathf.Rad2Deg;
        var snapAngle = snapFloatRotate * Mathf.Round(angle / snapFloatRotate);
        _currentElement.transform.rotation = Quaternion.Euler(0, 0, snapAngle);
        
        */
}