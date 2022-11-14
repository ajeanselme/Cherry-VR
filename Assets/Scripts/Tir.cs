using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    void Update()
    {
        //le missile qui avance
        transform.position += Vector3.up * 5 * Time.deltaTime;

        //le missile qui disparait un moment après
        Destroy(gameObject, 3);
    }

    //bon c'est pour toucher les aliens mais ça marche pas j'ai essayé pleins de trucs (genre trigger ou collision, collision ou collider..)
    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
        {
            Debug.Log("TIR");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }*/
}
