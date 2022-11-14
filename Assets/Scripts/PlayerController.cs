using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, block; 
    public GameObject missile;

    private float cooldown = 1;
  

    void Update()
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

        //tir avec un cd
        cooldown += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && cooldown > 1)
        {
            Instantiate(missile, transform.position, transform.rotation);
            cooldown = 0;
        }
    }

}
