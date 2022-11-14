using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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
    }

    void OnDisable()
    {
        MenuManager.OnPlay -= StartPlayer;
    }

    private void StartPlayer()
    {
        startPlayer = true;
        player.SetActive(startPlayer);
    }
}
