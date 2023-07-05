using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuRules : MonoBehaviour
{
    [SerializeField] private Button game, editor;

    private void Start()
    {
        game.onClick.AddListener(OnGameClick);
        editor.onClick.AddListener(OnEditorClick);
    }

    private void OnEditorClick()
    {
        SceneManager.LoadScene(2);
    }

    private void OnGameClick()
    {
        SceneManager.LoadScene(1);
    }
}
