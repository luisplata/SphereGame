using UnityEngine;
using UnityEngine.UI;

public class MenuRule : MonoBehaviour, IRule
{
    [SerializeField] private Animator menuAnimation;
    [SerializeField] private Button buttonMenu;
    [SerializeField] private Button buttonToCrateJson; 
    private bool _internalSwitcher;
    private IRulesMediator _rulesMediator;

    public void Config(IRulesMediator rulesMediator)
    {
        _rulesMediator = rulesMediator;
        buttonMenu.onClick.AddListener(OnMenuClick);
        buttonToCrateJson.onClick.AddListener(CreateJson);
    }

    private void CreateJson()
    {
        _rulesMediator.CreateJson();
    }

    private void OnMenuClick()
    {
        _internalSwitcher = !_internalSwitcher;
        menuAnimation.SetBool("IsOpen", _internalSwitcher);
    }
}