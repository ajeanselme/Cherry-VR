using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float block = 10.0f;

    public MissilePool missilePool;

    [SerializeField] VisualEffect muzzle;

    [Header("_______DEBUG______")]
    [SerializeField] float fireTimer = 0.0f;

    [Header("Recoil animation")] 
    public float duration = .1f;
    public int vibrato = 0;
    public float elasticity = 0;
    public float moveBackDuration = .1f;
    private float _baseY;


    private void Start()
    {
        _baseY = transform.position.y;
    }

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
                transform.position += Vector3.left * (speed * Time.deltaTime);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < block)
                transform.position += Vector3.right * (speed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (missilePool.Shoot())
            {
                muzzle.Play();
                PlayShootAnimation();
            }
        }
    }

    private void PlayShootAnimation()
    {
        Sequence shootRecoil = DOTween.Sequence();
        shootRecoil.Append(transform.DOPunchPosition(-transform.up, duration, vibrato, elasticity));
        shootRecoil.Append(transform.DOMoveY(_baseY, moveBackDuration));
        shootRecoil.Play();
    }

}
