using ServiceLocatorPath;
using UnityEngine;

public abstract class BaseElementInSceneWithCollider : BaseElementInScene{
    [SerializeField] protected CollisionCustom collision;
    [SerializeField] protected bool isTrigger;
    [SerializeField] protected Collider2D[] colliders;
    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
        collision.Config(this, isTrigger);
        collision.onCollisionValid = OnCollisionEnterBase;
        spriteRenderer.sprite = spriteOn;
        foreach (var collider in colliders)
        {
            collider.enabled = isTrigger;
        }
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