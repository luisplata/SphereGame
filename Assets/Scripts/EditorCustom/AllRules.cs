using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AllRules : MonoBehaviour, IRulesMediator
{
    [SerializeField, InterfaceType(typeof(IRule))]
    private Object[] rules;
    [SerializeField, InterfaceType(typeof(IFileManager))] private Object fileManager;
    [SerializeField, InterfaceType(typeof(IDrawDebugInEditor))] private Object drawDebugInEditor;
    public IDrawDebugInEditor DrawDebugInEditor
    {
        get => drawDebugInEditor as IDrawDebugInEditor;
    }
    

    private IFileManager _fileManager
    {
        get => fileManager as IFileManager;
    }
    private List<IRule> Rules
    {
        get
        {
            var localRule = new List<IRule>();
            foreach (var rule in rules)
            {
                localRule.Add(rule as IRule);
            }
            return localRule;
        }
    }

    private void Start()
    {
        foreach (var rule in Rules)
        {
            rule.Config(this);
        }
        _fileManager.Config(this);
        DrawDebugInEditor.Config(this);
    }

    public void CreateJson()
    {
        var result = GetRule<IFileManager>().CreateJson(GetRule<CreatorElementsRule>().DragComponents);
        ServiceLocator.Instance.GetService<IFileManager>().SaveMap(result);
        //Send to server or however you want
    }

    public List<DragComponent> GetListOfElements()
    {
        return GetRule<CreatorElementsRule>().DragComponents;
    }

    private T GetRule<T>()
    {
        foreach (var rule in Rules)
        {
            if (rule.GetType() == typeof(T))
            {
                return (T) rule;
            }
        }
        if(typeof(T) == typeof(IFileManager))
        {
            return (T) _fileManager;
        }
        if(typeof(T) == typeof(IDrawDebugInEditor))
        {
            return (T) DrawDebugInEditor;
        }
        throw new Exception($"No se encontro el {typeof(T)}");
    }
}