using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropRule : MonoBehaviour, IRule
{
    [SerializeField] private float snapFloat;
    [SerializeField] private SnapZone snapZone;
    private IRulesMediator _allRules;
    private bool _isClicked;
    private Vector2 _currentMouse = Vector2.zero;
    private GameObject _currentElement;
    private Vector3 _offset;
    private Vector3 _originalPosition;

    public void Config(IRulesMediator allRules)
    {
        _allRules = allRules;
    }
    
    public void OnPoint(InputAction.CallbackContext call)
    {
        _currentMouse = call.ReadValue<Vector2>();
        //Debug.Log($"_isClicked {_isClicked}");
        //Move current element in mouse position
        if (_isClicked && _currentElement != null)
        {
            var po = Camera.main.ScreenToWorldPoint(_currentMouse);
            _currentElement.transform.position = new Vector3(po.x, po.y, 0) + _offset;
        }
    }

    public void OnClick(InputAction.CallbackContext call)
    {
        if (call.started)
        {
            _isClicked = true;
            Debug.Log($"raycast {_currentMouse}");
            var ray = Camera.main.ScreenPointToRay(_currentMouse);
            foreach (var hit2D in Physics2D.RaycastAll(ray.origin, ray.direction, 1000f))
            {
                if (!hit2D.collider.gameObject.TryGetComponent<DragComponent>(out var drag)) continue;
                _currentElement = hit2D.collider.gameObject;
                _originalPosition = _currentElement.transform.position;
                //save offset from mouse to element
                var po = Camera.main.ScreenToWorldPoint(_currentMouse);
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
                        var po = Camera.main.ScreenToWorldPoint(_currentMouse);
                        var x = Mathf.Round(po.x / snapFloat) * snapFloat;
                        var y = Mathf.Round(po.y / snapFloat) * snapFloat;
                        _currentElement.transform.position = new Vector3(x, y, 0) + _offset;
                        break;
                    }
                    else
                    {
                        _currentElement.transform.position = _originalPosition;
                    }
                }
            }
            _currentElement = null;
        }
    }
}