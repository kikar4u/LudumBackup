using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        Debug.Log("GameOver");
    }
}
