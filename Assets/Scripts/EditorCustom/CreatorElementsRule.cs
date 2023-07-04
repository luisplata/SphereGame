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

    public void Config(IRulesMediator allRules)
    {
        _allRules = allRules;
        dropdown.onValueChanged.AddListener(OnDropdownChange);
        buttonToCreate.onClick.AddListener(OnButtonToCreateClick);
        //fulled dropdown
        dropdown.ClearOptions();
        dropdown.AddOptions(elements);
        Debug.Log("CreatorElementsRule: Config");
    }

    private void OnButtonToCreateClick()
    {
        var dragComponent = Instantiate(dragComponentPrefab, Vector3.zero, Quaternion.identity);
        dragComponent.ConfigureDragComponent(factoryOfElements.GetElementWithOutInstantate(elements[dropdown.value]));
    }

    private void OnDropdownChange(int arg0)
    {
        
    }
}