using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button ContinueGameButton;
    public LevelLoader loader;

    public void Start()
    {
        if (!DataManager.instance.HasGameData())
        {
            ContinueGameButton.interactable = false; 
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit!!!!");
        Application.Quit();
    }
    public void NewGame()
    {
        DisableAllButtons();
        DataManager.instance.NewGame();
        loader.LoadNextLevel(1);
        Cursor.visible = false;
    }
    public void ContinueGame()
    {
        DisableAllButtons();

        loader.LoadNextLevel(1);
        Cursor.visible = false;
    }
    private void DisableAllButtons()
    {
        newGameButton.interactable = false;
        ContinueGameButton.interactable = false;
    }
}
