using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    private Vector3 basePos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        basePos = transform.position;
    }

    public void ShakeCamera(float duration, Vector2 strength, int vibrato)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOShakePosition(duration, strength, vibrato));
        sequence.Append(transform.DOMove(basePos, 1f));
        sequence.Play();
    }
}
