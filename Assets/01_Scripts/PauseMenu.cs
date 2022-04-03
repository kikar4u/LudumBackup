using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenuUI;
    static bool WantsToQuit()
    {
        Debug.Log("Player prevented from quitting.");
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseMenuUI.SetActive(false);
        Debug.Log(GameStatus.PAUSED);
        
    }
    public void resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Debug.Log("Resume");
        Time.timeScale = 1;
        GameManager.instance.Status = GameStatus.RUNNING;
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        Application.wantsToQuit += WantsToQuit;
        Application.Quit();
    }
    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (Input.GetButton("Pause"))
        {
            pauseMenuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GameManager.instance.Status = GameStatus.PAUSED;
            Time.timeScale = 0;
        }
    }
}
