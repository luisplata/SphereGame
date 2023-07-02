using UnityEngine;

public abstract class BaseElementInSceneWithCollider : BaseElementInScene{
    [SerializeField] private CollisionCustom collision;
    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
        
        collision.onCollisionValid = (other)=>{
            OnCollisionEnterBase(other);
        };
    }
    protected abstract void OnCollisionEnterBase(GameObject other);
}