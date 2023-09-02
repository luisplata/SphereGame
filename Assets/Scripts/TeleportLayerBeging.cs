using UnityEngine;

public class TeleportLayerBeging : TeleportBeging
{
    [SerializeField] private int _layerDestiny;
    public void Config(ElementData element, ILogicOfLevel level, TeleportLayerEnd teleportEnd, int layerDestiny)
    {
        base.Config(element, level, teleportEnd);
        _layerDestiny = layerDestiny;
    }

    protected override void DoSomethingAfterTeleport()
    {
        base.DoSomethingAfterTeleport();
        _level.ChangeLayer(_layerDestiny);
    }
}