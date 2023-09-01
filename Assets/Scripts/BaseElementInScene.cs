using System.Collections;
using UnityEngine;

public abstract class BaseElementInScene : MonoBehaviour
{
    [SerializeField] protected string sfxName;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite spriteOn, spriteOff;
    protected int layer;
    protected ILogicOfLevel _level;

    public virtual void Config(ElementData element,ILogicOfLevel level)
    {
        transform.rotation = Quaternion.Euler(0, 0, element.Rotation);
        transform.position = new Vector2(element.PositionX, element.PositionY);
        layer = element.Layer;
        _level = level;
    }

    public bool IsValidLayer(){
        return layer == _level.GetCurrentLayer();
    }

    public int GetLayer()
    {
        return layer;
    }

    public void ChangeLayer(bool isMyLayer)
    {
        //set color to alpha .3 if isnt the same layer
        if(!isMyLayer){
            var color = Color.white;
            color.a = .3f;
            spriteRenderer.color = color;
        }
        else{
            var color = Color.white;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }
}