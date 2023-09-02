using UnityEngine;

public class TeleportLayerEnd : TeleportEnd
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _layerDestiny;
    
    public void Config(ElementData element, ILogicOfLevel level, int layerDestiny)
    {
        base.Config(element, level);
        layer = layerDestiny;
    }
}