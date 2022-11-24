using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [Header("Destroy screen shake")]
    public Vector2 shakeDirection = new Vector2(.1f, 0);

    public float shakeDuration = .1f;
    public int vibrato = 1;

    public EnemyText enemyText;

    [Header("_____Debug_____")]
    public bool isPendingKill = false;

    private void OnEnable()
    {
        isPendingKill = false;
    }

    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Kill()
    {
        isPendingKill = true;
        StartCoroutine(OnKill());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            Kill();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Missile"))
        {
            if(!isPendingKill)
                Kill();
        }
    }

    IEnumerator OnKill()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddEnemyDeath();
        }
        
        if(EffectManager.Instance.IsActivated("EnemyDeath"))
            CameraManager.Instance.ShakeCamera(shakeDuration, shakeDirection, vibrato);

        enemyText.Play();
        while (true)
        {
            if(!enemyText.IsPlaying())
            {
                break;
            }
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
