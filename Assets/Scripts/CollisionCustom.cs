using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionCustom : MonoBehaviour
{
    public Action<GameObject> onCollisionValid;
    [SerializeField] private BaseElementInScene element;

    internal BaseElementInScene GetElement()
    {
        return element;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && element.IsValidLayer()){
            onCollisionValid?.Invoke(other.gameObject);
        }
    }
}
