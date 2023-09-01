using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionCustom : MonoBehaviour
{
    public Action<GameObject> onCollisionValid;
    private BaseElementInScene element;
    
    public void Config(BaseElementInScene element)
    {
        this.element = element;
    }

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
