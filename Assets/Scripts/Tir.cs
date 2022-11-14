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
        // Add VFX + SFX here
    }

    private void OnDisable()
    {
        // Reset the missile to initial value
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
            //gameObject.SetActive(false);
            Deactivate();
        }
    }
    public void Deactivate()
    {
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
