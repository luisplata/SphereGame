using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreatorElementsRule : MonoBehaviour, IRule
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private List<string> elements;
    [SerializeField] private Button buttonToCreate;
    [SerializeField] private DragComponent dragComponentPrefab;
    [SerializeField] private FactoryOfElements factoryOfElements;
    private IRulesMediator _allRules;
    private List<DragComponent> _dragComponents = new();
    public List<DragComponent> DragComponents => _dragComponents;

    public void Config(IRulesMediator allRules)
    {
        _allRules = allRules;
        dropdown.onValueChanged.AddListener(OnDropdownChange);
        buttonToCreate.onClick.AddListener(OnButtonToCreateClick);
        //fulled dropdown
        dropdown.ClearOptions();
        dropdown.AddOptions(elements);
    }

    private void OnButtonToCreateClick()
    {
        var dragComponent = Instantiate(dragComponentPrefab, Vector3.zero, Quaternion.identity);
        _dragComponents.Add(dragComponent.ConfigureDragComponent(factoryOfElements.GetElementWithOutInstantate(elements[dropdown.value]),elements[dropdown.value]));
    }

    private void OnDropdownChange(int arg0)
    {
        
    }
}