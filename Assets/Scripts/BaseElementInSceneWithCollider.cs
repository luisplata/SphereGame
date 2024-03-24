using ServiceLocatorPath;
using UnityEngine;

public abstract class BaseElementInSceneWithCollider : BaseElementInScene{
    [SerializeField] protected CollisionCustom collision;
    [SerializeField] protected bool isTrigger;
    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
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