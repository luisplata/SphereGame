using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionCustom : MonoBehaviour
{
    public Action<GameObject> onCollisionValid;
    protected BaseElementInScene element;
    private bool _isTrigger;

    public void Config(BaseElementInScene element, bool isTrigger)
    {
        this.element = element;
        _isTrigger = isTrigger;
        GetComponent<Collider2D>().isTrigger = isTrigger;
    }

    internal BaseElementInScene GetElement()
    {
        return element;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && element.IsValidLayer() && !_isTrigger){
            onCollisionValid?.Invoke(other.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && element.IsValidLayer() && _isTrigger){
            onCollisionValid?.Invoke(other.gameObject);
        }
    }
    
    public bool IsTrigger()
    {
        return _isTrigger;
    }
}