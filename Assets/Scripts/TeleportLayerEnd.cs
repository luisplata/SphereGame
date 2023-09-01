﻿using Unity.Collections;
using UnityEngine;

public class TeleportLayerEnd : TeleportEnd
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField][ReadOnly] private int _layerDestiny;
    
    public void Config(ElementData element, ILogicOfLevel level, int layerDestiny)
    {
        base.Config(element, level);
        layer = layerDestiny;
    }
}