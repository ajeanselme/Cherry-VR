using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  trying to make it so it could be use with enemy too
 */
public class HealthSystem : MonoBehaviour
{
    public delegate void HealthEvent();
    public static event HealthEvent OnHealthZero;

    public int maxHealth = 10;

    [Header("_____DEBUG_____")]
    public int health = 10;

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if not colliding with himself
        if(!collision.gameObject.CompareTag(gameObject.tag))
        {
            Debug.Log(gameObject.tag + " collide with " + collision.gameObject.tag);
            health -= 1;

            if(health <= 0)
            {
                health = 0;

                if(gameObject.tag == "Player")
                {
                    if(OnHealthZero != null)
                    {
                        OnHealthZero.Invoke();
                    }
                }
            }
        }
    }
}
