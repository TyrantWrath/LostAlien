using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class playerMovement : CharacterMovement
{
    private float moveX, moveY;
    private Camera mainCam;
    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector3 tempScale;

    private Animator _animator;
    private Rigidbody2D _rigidBody2D;

    private PlayerWeaponManager _playerWeaponManager;
    private PlayerWeaponDurabilityManager _playerWeaponDurabilityManager;
    private CharacterHealth playerHealth;
    private CircleCollider2D _circleCollider2D;

    private CameraFadeOutScript _cameraFadeOutScript;
    private AudioManager _audioManager;

    protected override void Awake()
    {
        base.Awake();
        _audioManager = GameObject.FindGameObjectWithTag(TagManager.AUDIO_MANAGER_TAG).GetComponent<AudioManager>();

        mainCam = Camera.main;
        _cameraFadeOutScript = mainCam.GetComponent<CameraFadeOutScript>();

        _animator = GetComponent<Animator>();
        _playerWeaponManager = GetComponent<PlayerWeaponManager>();

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        playerHealth = GetComponent<CharacterHealth>();
        _playerWeaponDurabilityManager = GetComponent<PlayerWeaponDurabilityManager>();
    }
    private void FixedUpdate()
    {
        if (!playerHealth.IsAlive())
        {
            _cameraFadeOutScript.CameraFadeOutConditions(true, true, true);
            _audioManager.PlayeDeathFadeOutAudio();
            return;
            //PlayerDeathSystem();
        }
        moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        moveY = Input.GetAxisRaw(TagManager.VERTICAT_AXIS);
        HandlePlayerTurning();
        HandleMovement(moveX, moveY);
    }


    private void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        HandlePlayerAnimation(direction.x, direction.y);
    }

    void HandlePlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;
        if (x > 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (x < 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }
        transform.localScale = tempScale;
        x = Mathf.Abs(x);

        _animator.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, x);
        _animator.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, y);
        ActivateWeaponForSide(x, y);
    }

    void ActivateWeaponForSide(float x, float y)
    {
        if (x == 1f && y == 0f)
        {
            _playerWeaponManager.ActivateGun(0);
        }

        else if (x == 0f && y == 1f)
        {
            _playerWeaponManager.ActivateGun(1);
        }
        else if (x == 0f && y == -1f)
        {
            _playerWeaponManager.ActivateGun(2);
        }
        else if (x == 1f && y == 1f)
        {
            _playerWeaponManager.ActivateGun(3);
        }
        else if (x == 1f && y == -1f)
        {
            _playerWeaponManager.ActivateGun(4);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Intractable>())
        {
            WeaponIntractSO _intractableSO = col.GetComponent<Intractable>()._weaponIntractSO;

            if (!_intractableSO.IsHealthItem)
            {
                _audioManager.PlayMedKitPickUpAudio();
                _playerWeaponDurabilityManager.AddWeaponDurability(_intractableSO.DurabilityAmountTOAdd, _intractableSO.MyWeaponIndex);
            }

            else if (_intractableSO.IsHealthItem)
            {
                _audioManager.playWeaponPickUpAudio();
                playerHealth.HealthTOADD(_intractableSO.HealthAmountToAdd);
            }

            Destroy(col.gameObject);
        }

    }
}
