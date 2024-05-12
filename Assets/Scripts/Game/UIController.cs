using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject panelFather;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    private ILogicOfLevel _logic;

    public void Configure(ILogicOfLevel logic)
    {
        _logic = logic;
        panelFather.SetActive(false);
    }
    
    public void OnReset()
    {
        _logic.ResetGame();
        panelFather.SetActive(false);
    }

    public void ShowPanelWin()
    {
        //disable all panels before show the win panel
        panelFather.SetActive(true);
        panelWin.SetActive(true);
        panelLose.SetActive(false);
    }

    public void ShowPanelLose()
    {
        //disable all panels before show the lose panel
        panelFather.SetActive(true);
        panelWin.SetActive(false);
        panelLose.SetActive(true);
    }
    
    public void OnGoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}