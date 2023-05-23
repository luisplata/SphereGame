using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Action onSuccessPoint;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            onSuccessPoint?.Invoke();
        }
    }
}