using Unity.Collections;
using UnityEngine;

public class TeleportLayerBeging : TeleportBeging
{
    [SerializeField][ReadOnly] private int _layerDestiny;
    public void Config(ElementData element, ILogicOfLevel level, TeleportEnd teleportEnd, int layerDestiny)
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