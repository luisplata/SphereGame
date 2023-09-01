using UnityEngine;

public abstract class BaseElementInSceneWithCollider : BaseElementInScene{
    [SerializeField] protected CollisionCustom collision;
    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
        
        collision.onCollisionValid = OnCollisionEnterBase;
        
        spriteRenderer.sprite = spriteOn;
    }
    protected abstract void OnCollisionEnterBase(GameObject other);
}