using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeartUI : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();

    public void DeactivateHeart()
    {
        foreach(GameObject heart in hearts)
        {
            if(heart.activeInHierarchy)
            {
                heart.SetActive(false);
                break;
            }

        }
    }
}
