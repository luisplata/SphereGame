using UnityEngine;

public class SnapZone : MonoBehaviour
{
    public bool IsOk => _isOk;
    private bool _isOk;
    private void OnTriggerStay(Collider other)
    {
        _isOk = other.CompareTag("Element");
        Debug.Log($"SnapZone: OnTriggerStay {_isOk}");
    }
}