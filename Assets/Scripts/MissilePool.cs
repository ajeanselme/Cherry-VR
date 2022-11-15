using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    public int nbMissiles = 100;
    public GameObject missilePrefab;

    [Header("_____DEBUG_____")]
    public List<Tir> missiles = new List<Tir>();

    private void Awake()
    {
        // Instantiate Missiles for object pooling optimization
        for (int i = 0; i < nbMissiles; i++)
        {
            //GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            GameObject newMissile = Instantiate(missilePrefab, transform);
            Tir tir = newMissile.GetComponent<Tir>();
            missiles.Add(tir);
            newMissile.SetActive(false);
        }
    }

    public void Add(Tir missile)
    {

    }

    public void Activate()
    {
        foreach(Tir tir in missiles)
        {
            // si le missile est déja activé, skip le
            if(tir.isActiveAndEnabled)
            {
                continue;
            }

            // sinon active le et tir 1 fois
            tir.gameObject.SetActive(true);
            break;
        }
    }
}
