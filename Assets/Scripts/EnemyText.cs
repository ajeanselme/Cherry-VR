using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyText : MonoBehaviour
{
    public Canvas canvasEnemy;
    public Animation anim;
    public TMP_Text text;
    public List<string> cries = new List<string> ();
    private void OnEnable()
    {
        canvasEnemy.gameObject.SetActive(false);
    }

    public void Play()
    {
        canvasEnemy.gameObject.SetActive(true);
        int rng = Random.Range(0, cries.Count);
        text.text = cries[rng];
        //anim.Play();
    }

    public bool IsPlaying()
    {
        return anim.IsPlaying("Anim_EnemyText");
    }
}
