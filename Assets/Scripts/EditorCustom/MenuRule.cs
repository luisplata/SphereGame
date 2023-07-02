using UnityEngine;
using UnityEngine.UI;

public class MenuRule : MonoBehaviour, IRule
{
    [SerializeField] private Animator menuAnimation;
    [SerializeField] private Button buttonMenu;
    private bool _internalSwitcher;
    private IRulesMediator _rulesMediator;

    public void Config(IRulesMediator rulesMediator)
    {
        _rulesMediator = rulesMediator;
        buttonMenu.onClick.AddListener(OnMenuClick);
        Debug.Log("MenuRule: Config");
    }

    private void OnMenuClick()
    {
        Debug.Log("MenuRule: OnMenuClick");
        _internalSwitcher = !_internalSwitcher;
        menuAnimation.SetBool("IsOpen", _internalSwitcher);
    }
}