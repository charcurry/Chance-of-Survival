using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;
    ScoreManager scoreManager;
    public string previousScene;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        SceneLoadDebug();
    }

    public void LoadScene(string levelName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (levelName.StartsWith("Gameplay"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
        }
        //if (levelName == "Gameplay_Town1")
        //{
        //    foreach (GameObject gameObject in gameManager.level1Interactables)
        //    {
        //        gameObject.SetActive(true);
        //    }
        //    foreach (GameObject gameObject in gameManager.level2Interactables)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}
        //if (!levelName.StartsWith("Gameplay"))
        //{
        //    foreach (GameObject gameObject in gameManager.level1Interactables)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //    foreach (GameObject gameObject in gameManager.level2Interactables)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}
        //if (levelName == "Gameplay_Town2")
        //{
        //    foreach (GameObject gameObject in gameManager.level1Interactables)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //    foreach (GameObject gameObject in gameManager.level2Interactables)
        //    {
        //        gameObject.SetActive(true);
        //    }
        //}
        //foreach (InteractableObject item in gameManager.interactables)
        //{
        //    if (item.scene == levelName && item.active)
        //    {
        //        item.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        item.gameObject.SetActive(false);
        //    }
        //}
        if (levelName == "Settings")
        {
            gameManager.gameState = GameManager.GameState.Settings;
        }
        if (levelName == "Main Menu")
        {
            Time.timeScale = 1;
            scoreManager.ResetScore();
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        if (levelName == "GameOver")
        {
            gameManager.gameState = GameManager.GameState.GameOver;
        }
        SceneManager.LoadScene(levelName);
    }

    public void LoadPreviousScene()
    {
        SceneManager.sceneLoaded += OnPreviousSceneLoaded;
        if (previousScene.StartsWith("Gameplay"))
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
            Time.timeScale = 1;
        }
        if (previousScene == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        SceneManager.LoadScene(previousScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameManager.playerSprite.activeSelf == true)
        {
            gameManager.spawnPoint = GameObject.FindWithTag("SpawnPoint");
            gameManager.MovePlayerToSpawnPoint();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnPreviousSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //gameManager.MovePlayerToPreviousLocation();
        SceneManager.sceneLoaded -= OnPreviousSceneLoaded;
    }

    void SceneLoadDebug()
    {
        if (Input.GetKey(KeyCode.RightBracket))
        {
            Debug.Log("load next level");
            SceneManager.LoadScene(-SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKey(KeyCode.LeftBracket))
        {
            Debug.Log("load previous level");
            SceneManager.LoadScene(-SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}

