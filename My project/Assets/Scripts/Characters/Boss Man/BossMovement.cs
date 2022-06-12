using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float normalMovementSpeed = 2f;
    [SerializeField] private float playerDetectedMovementSpeed = 1.3f;

    private float moveSpeed;

    [SerializeField] private Transform[] movementPositions;

    private Vector3 targetPosition;
    private Vector3 _localScale;
    private bool playerDetected;
    private Transform playerTarget;

    private bool chasePlayer;

    [SerializeField] private float damageAmount = 15f;

    [SerializeField] private float shootTimerDelay = 2f;
    [SerializeField] private float shootTimer;

    [Header("CameraShake Handler")]
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeDuration;

    [SerializeField] private GameObject normalLight;
    [SerializeField] private GameObject chaseLight;
    [SerializeField] private GameObject bossGate;
    private EnemyShooterController _enemyShooterController;
    private CharacterHealth bossHealth;
    private CameraShake _cameraShake;

    private void Start()
    {
        moveSpeed = normalMovementSpeed;
        GetRandomMovementPosition();
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;

        _enemyShooterController = GetComponent<EnemyShooterController>();
        bossHealth = GetComponent<CharacterHealth>();
        _cameraShake = Camera.main.GetComponentInParent<CameraShake>();
    }

    private void Update()
    {
        if (!playerTarget || !bossHealth.IsAlive())
        {
            return;
        }
        HandleMovement();
        HandleFacingDirection();

        HandleShooting();
        HandleBossGlow();
    }

    private void OnDisable()
    {
        if (!bossHealth.IsAlive())
        {
            bossGate.SetActive(false);
        }
    }

    private void GetRandomMovementPosition()
    {
        int randomIndex = Random.Range(0, movementPositions.Length);

        while (targetPosition == movementPositions[randomIndex].position)
        {
            randomIndex = Random.Range(0, movementPositions.Length);

        }
        targetPosition = movementPositions[randomIndex].position;
    }

    void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            if (playerDetected)
            {
                if (Random.Range(0, 10) >= 7)
                {
                    targetPosition = playerTarget.position;
                    chasePlayer = true;

                }
            }

            else
            {
                if (!chasePlayer)
                {
                    GetRandomMovementPosition();

                }
            }

        }

    }
    private void HandleBossGlow()
    {
        if (chasePlayer)
        {
            chaseLight.SetActive(true);
            normalLight.SetActive(false);
        }
        else if (!chasePlayer)
        {
            chaseLight.SetActive(false);
            normalLight.SetActive(true);
        }

    }

    void HandleFacingDirection()
    {
        _localScale = transform.localScale;

        if (targetPosition.x > transform.position.x)
        {
            _localScale.x = Mathf.Abs(_localScale.x);
        }
        else if (targetPosition.x < transform.position.x)
        {
            _localScale.x = -Mathf.Abs(_localScale.x);
        }

        transform.localScale = _localScale;
    }

    void PlayerDetectedChangeMovementSpeed(bool detected)
    {
        if (detected)
        {

            moveSpeed = playerDetectedMovementSpeed;

        }
        else
        {
            moveSpeed = normalMovementSpeed;
        }
    }

    public void PlayerDetectedInfo(bool detected)
    {
        playerDetected = detected;

        PlayerDetectedChangeMovementSpeed(detected);

        if (!playerDetected)
        {
            chasePlayer = false;
            GetRandomMovementPosition();
        }
    }

    private void HandleShooting()
    {
        if (playerDetected)
        {
            if (Time.time > shootTimer)
            {
                shootTimer = Time.time + shootTimerDelay;
                Vector2 direction = (playerTarget.position - transform.position).normalized;
                _enemyShooterController.ShootOnRandom(direction, transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            chasePlayer = false;
            GetRandomMovementPosition();
            col.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
            _cameraShake.ShakeCamera(shakeIntensity, shakeDuration);
        }
    }


}
