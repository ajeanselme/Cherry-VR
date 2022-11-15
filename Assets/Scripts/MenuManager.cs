using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public delegate void PlayEvent();
    public static event PlayEvent OnPlay;

    public GameObject menu;
    public GameObject credits;
    public GameObject pause;
    public GameObject gameOver;

    [Header("____DEBUG_____")]
    [SerializeField] private bool isInGame = false;

    private void Awake()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
    }

    void OnEnable()
    {
        PlayerManager.OnDeath += GameOver;
    }

    void OnDisable()
    {
        PlayerManager.OnDeath -= GameOver;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Play()
    {
        menu.SetActive(false);
        gameOver.SetActive(false);
        isInGame = true;

        if (OnPlay != null)
            OnPlay();
    }

    public void Credits()
    {
        if(credits.activeInHierarchy)
        {
            credits.SetActive(false);
            
            if(isInGame)
            {
                pause.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
            }
        }
        else
        {
            credits.SetActive(true);

            if (isInGame)
            {
                pause.SetActive(false);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if(isInGame)
        {
            // Unpause game if is in Pause
            if (pause.activeInHierarchy)
            {
                Time.timeScale = 1.0f;
                pause.SetActive(false);
            }
            // Unpause game if is in Credits and input return
            else if(credits.activeInHierarchy)
            {
                credits.SetActive(false);
                Time.timeScale = 1.0f;
            }
            // Pause game
            else
            {
                pause.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    public void GameOver()
    {
        isInGame = false;
        gameOver.SetActive(true);
    }


    // temporary
    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }
}
