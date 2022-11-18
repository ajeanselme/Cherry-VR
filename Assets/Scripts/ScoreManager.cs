using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int enemyScoreValue = 75;
    public float multiplierDecreaseFactor = 10f;
    public int score { get; private set; } = 0;

    [Header("Score zoom punch")] 
    public Vector2 zoomDirection = Vector2.one;
    public float duration = .1f;
    public int vibrato = 10;
    public float elasticity = 1;

    public MultiplierStep[] multiplierSteps;

    private int _currentMultiplier;
    private float _multiplierProgress;
    private float _multiplierProgressTarget;

    private float _lerpInterpolator;

    public TMP_Text scoreText;
    public Image multiplierFiller;

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
        multiplierFiller.color = multiplierSteps[0].barColor;
        multiplierFiller.fillAmount = 0;
    }

    private void Update()
    {
        if (_multiplierProgress > 0)
        {
            _multiplierProgress -= Time.deltaTime * multiplierDecreaseFactor;
            multiplierFiller.fillAmount = Mathf.Lerp( multiplierFiller.fillAmount,  _multiplierProgress / _multiplierProgressTarget, _lerpInterpolator);
            _lerpInterpolator += .2f * Time.deltaTime;
            if (_lerpInterpolator > 1)
            {
                _lerpInterpolator = 0;
            }
        }
        else
        {
            _lerpInterpolator = 0f;
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
        Sequence sequence = DOTween.Sequence();
        sequence.Append(scoreText.transform.DOPunchScale(zoomDirection, duration, vibrato, elasticity));
        sequence.Play();
        
        if(_multiplierProgress >= _multiplierProgressTarget)
            IncreaseMultiplier();
    }

    private void IncreaseMultiplier()
    {
        if (multiplierSteps.Length > _currentMultiplier)
        {
            _currentMultiplier += 1;
            _multiplierProgressTarget = multiplierSteps[_currentMultiplier].scoreRequired;
            _multiplierProgress = _multiplierProgressTarget / 16f;
            multiplierFiller.color = multiplierSteps[_currentMultiplier].barColor;
        }
    }
    private void DecreaseMultiplier()
    {
        if (_currentMultiplier > 0)
        {
            _currentMultiplier -= 1;
            _multiplierProgressTarget = multiplierSteps[_currentMultiplier].scoreRequired;
            _multiplierProgress = _multiplierProgressTarget / 20f;
            multiplierFiller.color = multiplierSteps[_currentMultiplier].barColor;
        }
    }

    [Serializable]
    public struct MultiplierStep
    {
        public float scoreRequired;
        public Color barColor;
    }
    
    

}
