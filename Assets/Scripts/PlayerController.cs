using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float block = 10.0f;
    public float fireRate = 0.2f;

    public MissilePool missilePool;

    [Header("_______DEBUG______")]
    [SerializeField] float fireTimer = 0.0f;

    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        //gauche droite avec une limitation (block)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -block)
                transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < block)
                transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    private void Shoot()
    {
        //tir avec un cooldown
        fireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer > fireRate)
        {
            missilePool.Activate();
            fireTimer = 0.0f;
        }
    }

}
