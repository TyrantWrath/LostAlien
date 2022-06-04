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
    private CharacterHealth playerHealth;
    private Collider2D[] _myColliders;

    private CameraFadeOutScript _cameraFadeOutScript;
    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
        _cameraFadeOutScript = mainCam.GetComponent<CameraFadeOutScript>();
        _animator = GetComponent<Animator>();
        _playerWeaponManager = GetComponent<PlayerWeaponManager>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _myColliders = GetComponents<Collider2D>();
    }

    private void Start()
    {
        playerHealth = GetComponent<CharacterHealth>();
    }
    private void FixedUpdate()
    {
        if (!playerHealth.IsAlive())
        {
            _cameraFadeOutScript.CameraFadeOutConditions(true);
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

        if (x == 0f && y == 1f)
        {
            _playerWeaponManager.ActivateGun(1);
        }
        if (x == 0f && y == -1f)
        {
            _playerWeaponManager.ActivateGun(2);
        }
        if (x == 1f && y == 1f)
        {
            _playerWeaponManager.ActivateGun(3);
        }
        if (x == 1f && y == -1f)
        {
            _playerWeaponManager.ActivateGun(4);
        }
    }

    /*private void PlayerDeathSystem()
    {
        _animator.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
        _rigidBody2D.velocity = Vector2.zero;
        for (int i = 0; i < _myColliders.Length; i++)
        {
            _myColliders[i].enabled = false;
        }

    }*/
}
