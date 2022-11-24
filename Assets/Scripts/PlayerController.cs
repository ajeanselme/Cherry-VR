using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float block = 10.0f;

    public GameObject TrailLeft, TrailRight;
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

    public InputActionProperty moveInput;
    public InputActionProperty shootInput;
    private bool move;
    private bool shoot;

    [Header("_____SOUND_____")]
    public string shootSound = "";
    private void Start()
    {
        _baseY = transform.position.y;
    }

    void Update()
    {
        HandleInput();
        Move();
        Shoot();
    }

    private void Move()
    {
        if (move)
        {
            if (moveInput.action.ReadValue<Vector2>().x > 0 && transform.position.x < block)
            {
                transform.position += new Vector3(moveInput.action.ReadValue<Vector2>().x * (speed * Time.deltaTime), 0);
                TrailLeft.SetActive(true);
                TrailRight.SetActive(false);
            }
            else if (moveInput.action.ReadValue<Vector2>().x < 0 && transform.position.x > -block)
            {
                transform.position += new Vector3(moveInput.action.ReadValue<Vector2>().x * (speed * Time.deltaTime), 0);
                TrailRight.SetActive(true);
                TrailLeft.SetActive(false);
            }
            else
            {
                TrailLeft.SetActive(false);
                TrailRight.SetActive(false);
            }
        }
        else
        {
            TrailLeft.SetActive(false);
            TrailRight.SetActive(false);
        }
    }

    private void Shoot()
    {
        if (shoot)
        {
            if (missilePool.Shoot())
            {
                if(muzzle != null)
                    muzzle.Play();
                PlayShootAnimation();
                SoundManager.Instance.PlaySound(shootSound);
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

    private void HandleInput()
    {
        if (moveInput.action.WasPressedThisFrame())
            move = true;
        if (moveInput.action.WasReleasedThisFrame())
            move = false;
        
        if (shootInput.action.WasPressedThisFrame())
            shoot = true;
        else
            shoot = false;
    }
}

