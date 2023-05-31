using System;
using UnityEngine;

public class CollisionCustom : MonoBehaviour
{
    public Action<GameObject> onCollisionValid;
    [SerializeField] private BaseElementInScene element;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && element.IsValidLayer()){
            onCollisionValid?.Invoke(other.gameObject);
        }
    }
}
