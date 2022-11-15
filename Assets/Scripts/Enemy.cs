using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnDisable()
    {
        
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
