using System.Collections;
using DG.Tweening;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            Deactivate();
            //gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Alien collide with Player");

            Deactivate();
            //gameObject.SetActive(false);
        }
    }

    private void Deactivate()
    {
        if(gameObject.activeInHierarchy)
            StartCoroutine(OnDeactivate());

        Sequence destroySequence = DOTween.Sequence();
        destroySequence.Append(Camera.main.DOShakePosition(shakeDuration, shakeDirection, vibrato));
        destroySequence.Play();
    }

    IEnumerator OnDeactivate()
    {
        // Begin here any VFX & SFX & Feel

        //while(true)
        //{
        //    // Add here conditions to break loop
        //    //if()
        //    //{
        //    //    break;
        //    //}

        //    yield return null;
        //}
        yield return null;

        gameObject.SetActive(false);
    }
}
