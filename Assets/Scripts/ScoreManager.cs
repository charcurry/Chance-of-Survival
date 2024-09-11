using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI gamePlayHighScore;
    public TextMeshProUGUI gamePlayPlayerScore;
    public TextMeshProUGUI gameOverHighScore;
    public TextMeshProUGUI gameOverPlayerScore;
    public int playerScore;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
       if (playerScore > highScore) 
        {
            PlayerPrefs.SetInt("highScore", playerScore);
        }
        highScore = PlayerPrefs.GetInt("highScore");

        gamePlayPlayerScore.text = "Score: " + playerScore.ToString();
        gameOverPlayerScore.text = "Your Score: " + playerScore.ToString();
        gamePlayHighScore.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
        gameOverHighScore.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
    }
}
