using ServiceLocatorPath;
using UnityEngine;

public abstract class BaseElementInSceneWithCollider : BaseElementInScene{
    [SerializeField] protected CollisionCustom collision;
    [SerializeField] protected bool isTrigger;
    [SerializeField] protected Collider2D[] colliders;
    public override void Config(ElementData element, ILogicOfLevel level, bool moveTransform = true)
    {
        base.Config(element, level, moveTransform);
        collision.Config(this, isTrigger);
        collision.onCollisionValid = OnCollisionEnterBase;
        spriteRenderer.sprite = spriteOn;
    }

    protected virtual void OnCollisionEnterBase(GameObject other)
    {
        ServiceLocator.Instance.GetService<ISoundSfxService>().PlaySound(sfxName);
    }
    
    
    public bool IsTrigger()
    {
        return isTrigger;
    }
}