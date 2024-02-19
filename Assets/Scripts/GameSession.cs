using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Drawing;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives=3;
    [SerializeField] int score =0;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession>1)
        {
            Destroy (gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        liveText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int pointsToAdd)
    {
        score +=pointsToAdd;
        scoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives >1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    private void TakeLife()
    {
        int currentScene= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        playerLives--;
        liveText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScene();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    
}
