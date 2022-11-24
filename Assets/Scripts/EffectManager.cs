using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    
    /* Contains disabled effects */
    private static List<string> effectMapActivation = new List<string>();

    public GameObject EffectPanel;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        EffectPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            EffectPanel.SetActive(!EffectPanel.activeSelf);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            bool activated = toggleEffect("ShootAnimation");
            EffectPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "(" + activated + ") ShootAnimation [1]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            bool activated = toggleEffect("ScoreDetailAnimation");
            EffectPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = "(" + activated + ") ScoreDetailAnimation [2]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            bool activated = toggleEffect("PunchZoomScore");
            EffectPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = "(" + activated + ") PunchZoomScore [3]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            bool activated = toggleEffect("ShakeBar");
            EffectPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = "(" + activated + ") ShakeBar [4]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            bool activated = toggleEffect("RescaleBar");
            EffectPanel.transform.GetChild(4).GetComponent<TMP_Text>().text = "(" + activated + ") RescaleBar [5]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            bool activated = toggleEffect("EnemyDeath");
            EffectPanel.transform.GetChild(5).GetComponent<TMP_Text>().text = "(" + activated + ") EnemyDeath [6]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            bool activated = toggleEffect("PlayerTakeDamage");
            EffectPanel.transform.GetChild(6).GetComponent<TMP_Text>().text = "(" + activated + ") PlayerTakeDamage [7]";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            bool activated = toggleEffect("FinalScoreFillAnimation");
            EffectPanel.transform.GetChild(7).GetComponent<TMP_Text>().text = "(" + activated + ") FinalScoreFillAnimation [8]";
        }
    }

    public bool IsActivated(string id)
    {
        return !effectMapActivation.Contains(id);
    }

    public bool toggleEffect(string id)
    {
        bool isActivated = IsActivated(id);
        if (isActivated)
            effectMapActivation.Add(id);
        else
        {
            effectMapActivation.Remove(id);
        }

        return !isActivated;
    }
    
    
}
