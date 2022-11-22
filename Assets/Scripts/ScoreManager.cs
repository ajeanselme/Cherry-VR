using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int enemyScoreValue = 75;
    public int score { get; private set; } = 0;

    [Header("Score zoom punch")] 
    public Vector2 zoomDirection = Vector2.one;
    public float zoomDuration = .1f;
    public int zoomVibrato = 10;
    public float zoomElasticity = 1;

    [Header("Multiplier")] 
    public MultiplierStep[] multiplierSteps;
    public Image multiplierFiller;
    public float barFillDuration = .2f;
    public float multiplierDecreaseFactor = 10f;
    
    private MultiplierStep _currentMultiplierStep;
    private int _currentMultiplier;
    private float _multiplierProgress;
    private float _multiplierProgressTarget;

    public TMP_Text scoreText;
    public TMP_Text multiplierText;

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
        scoreText.text = "";
        _multiplierProgress = 0;
        _multiplierProgressTarget = multiplierSteps[0].scoreRequired;
        _currentMultiplierStep = multiplierSteps[0];
        multiplierFiller.color = multiplierSteps[0].barColor;
        multiplierFiller.fillAmount = 0;
        multiplierText.text = "";
    }

    private void Update()
    {
        if (_multiplierProgress > 0)
        {
            _multiplierProgress -= Time.deltaTime * multiplierDecreaseFactor;
            multiplierFiller.DOFillAmount(_multiplierProgress / _multiplierProgressTarget, barFillDuration);
        }
        else
        {
            DecreaseMultiplier();
        }
    }

    public void AddEnemyDeath()
    {
        AddScore(enemyScoreValue);
    }

    private void AddScore(int value)
    {
        score += value;
        _multiplierProgress += value;
        scoreText.text = score.ToString();
        PunchZoomScore();
        if(_multiplierProgress >= _multiplierProgressTarget)
            IncreaseMultiplier();
    }

    private void IncreaseMultiplier()
    {
        if (multiplierSteps.Length > _currentMultiplier + 1)
        {
            _currentMultiplier += 1;
            _currentMultiplierStep = multiplierSteps[_currentMultiplier];
            UpdateMultiplierBar();
        }
    }
    private void DecreaseMultiplier()
    {
        if (_currentMultiplier > 0)
        {
            _currentMultiplier -= 1;
            _currentMultiplierStep = multiplierSteps[_currentMultiplier];
            UpdateMultiplierBar(false);
        }
    }

    private void UpdateMultiplierBar(bool shakeBar = true)
    {
        _multiplierProgressTarget = _currentMultiplierStep.scoreRequired;
        _multiplierProgress = _multiplierProgressTarget / 20f;
        multiplierFiller.color = _currentMultiplierStep.barColor;
            
        multiplierFiller.transform.parent.DOScale(_currentMultiplierStep.barScale,
            _currentMultiplierStep.barScaleDuration);
        if (_currentMultiplier > 0)
            multiplierText.text = "x" + _currentMultiplier;
        else
            multiplierText.text = "";

        if(shakeBar)
            ShakeBar();
    }

    private void PunchZoomScore()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(scoreText.transform.DOPunchScale(zoomDirection, zoomDuration, zoomVibrato, zoomElasticity));
        sequence.Append(scoreText.transform.DOScale(Vector3.one, .1f));
        sequence.Play();
    }
    
    private void ShakeBar()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(multiplierFiller.gameObject.transform.parent.DOShakeRotation(
            _currentMultiplierStep.barShakeDuration, 
            new Vector3(0, 0, _currentMultiplierStep.barShakeForce),
            _currentMultiplierStep.barShakeVibrato));
        sequence.Append(multiplierFiller.gameObject.transform.parent.DORotate(Vector3.zero, .05f));
        sequence.Play();
    }

    [Serializable]
    public class MultiplierStep
    {
        public float scoreRequired;
        public Color barColor;

        public Vector2 barScale = Vector2.one;
        public float barScaleDuration = .2f;
        public float barShakeForce;
        public float barShakeDuration = 1f;
        public int barShakeVibrato = 10;
    }
}
