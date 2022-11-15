using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void PlayerEvent();
    public static event PlayerEvent OnDeath;

    public GameObject player;

    [Header("______DEBUG_______")]
    [SerializeField] private bool startPlayer = false;

    private void Awake()
    {
        player.SetActive(startPlayer);
    }

    void OnEnable()
    {
        MenuManager.OnPlay += StartPlayer;
        HealthSystem.OnHealthZero += EndPlayer;
    }

    void OnDisable()
    {
        MenuManager.OnPlay -= StartPlayer;
        HealthSystem.OnHealthZero -= EndPlayer;
    }

    private void StartPlayer()
    {
        startPlayer = true;
        player.SetActive(true);
    }

    private void EndPlayer()
    {
        startPlayer = false;
        player.SetActive(false);

        if(OnDeath != null)
        {
            OnDeath.Invoke();
        }
    }
}
