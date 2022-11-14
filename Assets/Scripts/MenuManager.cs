using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;
    public GameObject pause;

    [Header("____DEBUG_____")]
    [SerializeField] private bool isInGame = false;

    private void Awake()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        pause.SetActive(false);
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
        isInGame = true;
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
}
