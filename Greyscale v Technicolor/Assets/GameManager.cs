using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("the number of receivers that need to be active")]
    public int maxReceivers = 3;
    [Tooltip("the currently active receivers")]
    public List<ColorReceiver> activeReceivers;
    [Tooltip("If it's paused")]
    public bool paused = false;
    [Tooltip("If the game has started")]
    public bool gameStarted = false;

    [Header("Canvas")]
    [Tooltip("Start game panel")]
    public GameObject startCanvas;
    [Tooltip("Pause game panel")]
    public GameObject pauseCanvas;
    [Tooltip("Game Over panel")]
    public GameObject gameOverCanvas;

    [Tooltip("the instance of the manager")]
    private static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        
        //Before enabling the object, we need to make sure that there are no other GameManagers.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        activeReceivers = new List<ColorReceiver>();
    }

    void Start()
    {
        Time.timeScale = 0;
        startCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager Instance()
    {
        return instance;
    }

    public void AddReceiver(ColorReceiver newRec)
    {
        if (!activeReceivers.Contains(newRec))
        {
            activeReceivers.Add(newRec);
            if(activeReceivers.Count >= maxReceivers)
            {
                GameOver();
            }
        }
    }

    public void RemoveReceiver(ColorReceiver rec)
    {
        if (activeReceivers.Contains(rec))
        {
            activeReceivers.Remove(rec);
        }
    }

    public void Pause()
    {
        if (!gameStarted)
        {
            return;
        }
        if (paused)
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        startCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gameStarted = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
