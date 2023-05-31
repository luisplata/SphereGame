using UnityEngine;

public abstract class BaseElementInScene : MonoBehaviour
{
    private int layer;
    protected ILogicOfLevel _level;

    public virtual void Config(ElementData element,ILogicOfLevel level)
    {
        transform.rotation = Quaternion.Euler(0, 0, element.Rotation);
        transform.position = new Vector2(element.PositionX, element.PositionY);
        layer = element.Layer;
        _level = level;
    }

    public virtual bool IsValidLayer(){
        return layer == _level.GetCurrentLayer();
    }

    public int GetLayer()
    {
        return layer;
    }
}

public abstract class BaseElementInSceneWithCollider : BaseElementInScene{
    [SerializeField] private CollisionCustom collision;
    public override void Config(ElementData element, ILogicOfLevel level)
    {
        base.Config(element, level);
        
        collision.onCollisionValid = (other)=>{
            OnCollisionEnter(other);
        };
    }
    protected abstract void OnCollisionEnter(GameObject other);
}