using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject PauseUI;
    public GameObject GameplayUI;
    public GameObject SettingsUI;
    public GameObject GameOverUI;
    public GameObject GameWinUI;

    // Start is called before the first frame update

    public void UIMainMenu()
    {
        MainMenuUI.SetActive(true);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }

    public void UIPause()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(true);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }

    public void UIGameplay()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(true);
        SettingsUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }

    public void UISettings()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(true);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);

    }

    public void UIGameOver()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        GameOverUI.SetActive(true);
        GameWinUI.SetActive(false);
    }

    public void UIGameWin()
    {
        MainMenuUI.SetActive(false);
        PauseUI.SetActive(false);
        GameplayUI.SetActive(false);
        SettingsUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(true);
    }
}
