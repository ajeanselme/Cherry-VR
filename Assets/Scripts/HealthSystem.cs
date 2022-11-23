using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Sequence = DG.Tweening.Sequence;

/*
 *  trying to make it so it could be use with enemy too
 */
public class HealthSystem : MonoBehaviour
{
    public VolumeProfile postProcessProfile;
    private Vignette _vignette;
    public delegate void HealthEvent();
    public static event HealthEvent OnPlayerDeath;

    public int maxHealth = 10;

    [Header("On damage")] 
    public float timeToFullRedVignette = 1f;
    public float timeToRetractRedVignette = 2f;
    public float fullVignetteIntensity = .7f;
    public float damageScreenShakeDuration = .5f;
    public int damageScreenShakeVibrato = 10;
    public Vector2 damageScreenShakeStrength = Vector2.one;

    [Header("_____DEBUG_____")]
    public int health = 10;

    private void Awake()
    {
        health = maxHealth;
        if(postProcessProfile != null)
        {
            foreach (var volumeComponent in postProcessProfile.components)
            {
                if (volumeComponent is Vignette vignette)
                {
                    _vignette = vignette;
                    _vignette.intensity.value = 0f;
                }
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if not colliding with himself
        if(!collision.gameObject.CompareTag(gameObject.tag))
        {
            Debug.Log(gameObject.tag + " collide with " + collision.gameObject.tag);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health -= 1;
        if (health > 0)
        {
            PlayPlayerTakeDamage();
        }
        else
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void PlayPlayerTakeDamage()
    {
        CameraManager.Instance.ShakeCamera(damageScreenShakeDuration, damageScreenShakeStrength, damageScreenShakeVibrato);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, fullVignetteIntensity, timeToFullRedVignette));
        sequence.Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, 0, timeToRetractRedVignette));
        sequence.Play();
    }
}
