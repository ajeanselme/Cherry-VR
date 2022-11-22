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

    public void Kill()
    {
        ScoreManager.Instance.AddEnemyDeath();
        gameObject.SetActive(false);

        CameraManager.Instance.ShakeCamera(shakeDuration, shakeDirection, vibrato);
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
    }
}
