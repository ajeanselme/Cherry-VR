using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleAnimationOdds : MonoBehaviour
{
    public int idleOdds;
    private Animator animatotor;
    public bool speeding;

    // Start is called before the first frame update
    void Start()
    {
        animatotor = gameObject.GetComponent<Animator>();
        speeding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (speeding)
        {
            animatotor.speed += 1;
        }
    }

    //à chaque loop de l'anim idle de base il calcule un chiffre ; selon sa valeur sera choisie la prochaine anim idle jouée
    //(ça rajoute de l'aléatoire)
    public void calculOdds ()
    {
        idleOdds = Random.Range(0, 10);
        animatotor.SetInteger("odds", idleOdds);
    }

    //quand l'ennemi meurt, il gigote de gauche à droite; si speeding est vrai, aka si le perso meurt, il bougera de plus en plus vite
    //(le destroy de l'ennemi n'est pas compris dans le code)
    public void deathAnim ()
    {
        speeding = true;
    }
}
