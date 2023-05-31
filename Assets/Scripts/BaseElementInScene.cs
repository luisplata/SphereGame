using UnityEngine;

public abstract class BaseElementInScene : MonoBehaviour
{
    private int layer;

    public virtual void Config(ElementData element)
    {
        transform.rotation = Quaternion.Euler(0, 0, element.Rotation);
        transform.position = new Vector2(element.PositionX, element.PositionY);
        layer = element.Layer;
    }

    public int GetLayer()
    {
        return layer;
    }
}