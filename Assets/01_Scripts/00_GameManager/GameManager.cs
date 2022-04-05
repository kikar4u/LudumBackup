using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver = false;
    GameStatus status = GameStatus.RUNNING;

    private int gameScore;

    public GameStatus Status { get => status; set => status = value; }
    public int GameScore { get => gameScore; set => gameScore = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : GameManager");
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("01_Placeholder");
            Time.timeScale = 1.0f;
            gameOver = false;
        }
    }

    public void UpdateScore(int increment)
    {
        GameScore += increment;
        CanvasManager.instance.UpdateScoreText(GameScore);
    }

    public void UpdateStarvingUI(float starvingPoint)
    {
        CanvasManager.instance.UpdateStarvingSlider(starvingPoint);
    }

    public void StartIndegestionUI(float indigestionTime)
    {
        CanvasManager.instance.UpdateIndegestionSlider(indigestionTime);
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        CanvasManager.instance.transform.GetChild(CanvasManager.instance.transform.childCount - 1).gameObject.SetActive(true);
        gameOver = true;
        Debug.Log("GameOver");
    }
}
