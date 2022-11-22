using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public InputActionProperty moveInput;
    public InputActionProperty shootInput;
    private bool move;
    private bool shoot;

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
            if (transform.position.x < block || transform.position.x > -block)
                transform.position += new Vector3(moveInput.action.ReadValue<Vector2>().x * (speed * Time.deltaTime), 0);

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
