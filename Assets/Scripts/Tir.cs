using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public float speed = 100.0f;
    public float disappearTime = 1.0f;

    [Header("_________DEBUG_______")]
    [SerializeField] private float disappearTimer = 0.0f;

    private void OnEnable()
    {
 
    }

    private void OnDisable()
    {
        disappearTimer = 0.0f;
        transform.position = transform.parent.position;
    }

    void Update()
    {
        //le missile qui avance
        transform.position += Vector3.up * speed * Time.deltaTime;

        //le missile qui disparait un moment après
        disappearTimer += Time.deltaTime;
        if(disappearTimer > disappearTime)
        {
            gameObject.SetActive(false);
        }
    }
}
