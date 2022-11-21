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
    
    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnDisable()
    {
        ScoreManager.Instance.AddEnemyDeath();

        Sequence destroySequence = DOTween.Sequence();
        destroySequence.Append(Camera.main.DOShakePosition(shakeDuration, shakeDirection, vibrato));
        destroySequence.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
