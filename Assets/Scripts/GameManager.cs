using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;

    private PlayerMovement player;
    public GameObject playerSprite;
    public GameObject spawnPoint;
    //public Vector2 lastPlayerPosition;

    public enum GameState
    {
        MainMenu,
        Gameplay,
        Settings,
        Pause,
        GameOver,
        Credits
    }

    public GameState gameState;
    public GameState previousGameState;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindWithTag("SpawnPoint");
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerMovement>();
        gameState = GameState.MainMenu;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        switch (gameState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Gameplay:
                Gameplay();
                break;
            case GameState.Pause:
                Pause();
                break;
            case GameState.Settings:
                Settings();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.Credits:
                Credits();
                break;
        }
    }

    private void MainMenu()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIMainMenu();
    }

    private void Gameplay()
    {
        Cursor.visible = true;
        playerSprite.SetActive(true);
        //lastPlayerPosition = player.transform.position;
        uiManager.UIGameplay();
    }

    private void Settings()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UISettings();
    }

    private void Pause()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIPause();
    }

    private void GameOver()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UIGameOver();
    }

    private void Credits()
    {
        Cursor.visible = true;
        playerSprite.SetActive(false);
        uiManager.UICredits();
    }

    public void PauseGame()
    {
        if (gameState != GameState.Pause && gameState == GameState.Gameplay)
        {
            previousGameState = gameState;
            gameState = GameState.Pause;
            Time.timeScale = 0;
        }
        else if (gameState == GameState.Pause || gameState == GameState.Settings)
        {
            gameState = previousGameState;
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        if (gameState != GameState.Pause)
        {
            previousGameState = gameState;
        }
        gameState = GameState.Settings;
    }

    public void OpenCredits()
    {
        gameState = GameState.Credits;
    }

    public void MovePlayerToSpawnPoint()
    {
        player.transform.position = spawnPoint.transform.position;
    }

    public void MovePlayerToPreviousLocation()
    {
        //player.transform.position = lastPlayerPosition;
    }
    public void Back()
    {
        if (previousGameState != GameState.MainMenu)
        {
            gameState = GameState.Pause;
        }
        else
        {
            gameState = previousGameState;
            Time.timeScale = 1;
        }
    }

}
