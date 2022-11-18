using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int enemyScoreValue = 75;
    public int score { get; private set; } = 0;

    [Header("Score zoom punch")] 
    public Vector2 zoomDirection = Vector2.one;
    public float duration = .1f;
    public int vibrato = 10;
    public float elasticity = 1;
    
    
    public TMP_Text scoreText;

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
    }

    public void AddEnemyDeath()
    {
        AddScore(enemyScoreValue);
    }

    private void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(scoreText.transform.DOPunchScale(zoomDirection, duration, vibrato, elasticity));
        sequence.Play();
    }
}
