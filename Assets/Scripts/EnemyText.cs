using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyText : MonoBehaviour
{
    public Canvas canvasEnemy;
    public Animation anim;
    private void OnEnable()
    {
        canvasEnemy.gameObject.SetActive(false);
    }

    public void Play()
    {
        canvasEnemy.gameObject.SetActive(true);
        //anim.Play();
    }

    public bool IsPlaying()
    {
        return anim.IsPlaying("Anim_EnemyText");
    }
}
