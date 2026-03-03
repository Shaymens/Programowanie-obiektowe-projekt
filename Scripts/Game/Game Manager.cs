using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameActive = true;
        gameTime = 0f;
    }

    void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            NewMonoBehaviourScript.Instance.UpdateTimer(gameTime);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }

    public void GameOver()
        {
        gameActive = false;
        NewMonoBehaviourScript.Instance.gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        if (NewMonoBehaviourScript.Instance.pausePanel.activeSelf == false && !NewMonoBehaviourScript.Instance.gameOverPanel.activeSelf)
        {
            NewMonoBehaviourScript.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            NewMonoBehaviourScript.Instance.pausePanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

 


}
