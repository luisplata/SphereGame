using System;
using UnityEngine;
using UnityEngine.UI;

public class PrinterDebugInEditorMode : MonoBehaviour, IDrawDebugInEditor
{
    [SerializeField] private Button buttonToSwitchDebug;
    [SerializeField] private LineRenderer lineRenderer;
    private bool _internalSwitcher;
    private IRulesMediator _rulesMediator;

    public void Config(IRulesMediator allRules)
    {
        _rulesMediator = allRules;
        buttonToSwitchDebug.onClick.AddListener(OnButtonToSwitchDebugClick);
    }

    private void OnButtonToSwitchDebugClick()
    {
        _internalSwitcher = !_internalSwitcher;
        lineRenderer.enabled = _internalSwitcher;
    }

    private void FixedUpdate()
    {
        if (!_internalSwitcher) return;
        var listOfElements = _rulesMediator.GetListOfElements();
        lineRenderer.positionCount = listOfElements.Count;
        for(var i = 0; i < listOfElements.Count; i++)
        {
            lineRenderer.SetPosition(i, listOfElements[i].transform.position);
        }
    }
}