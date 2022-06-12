using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement
{
    // To be deleted
    public bool CHASE_PLAYER;

    private Transform playerTarget;
    private Vector3 playerLastKnownPosition;
    private Vector3 startingPosition;
    private Vector3 enemyMovementMotion;

    private bool dealthDamageToPlayer;

    [SerializeField] private float damageCoolDownThreshold = 1f;
    private float damageCoolDownTimer;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float chaseSpeed = 0.8f;

    [SerializeField] private float enemyShakeIntensity = 0.4f;
    [SerializeField] private float enemyShakeDuration = 0.2f;
    [SerializeField] private Vector2 knockBackEffect = new Vector2(2f, 2f);

    private float lastFollowTime;
    private float turningTimeDelay = 1f;
    [SerializeField] private float turningDelayRate;

    private Vector3 myScale;
    private SpriteRenderer _spriteRenderer;
    private CharacterHealth enemyHealth;
    private EnemyBatchHandler _enemyBatch;
    private CameraShake _cameraShake;

    private Collider2D[] _collider2D;


    protected override void Awake()
    {
        base.Awake();
        _cameraShake = Camera.main.GetComponentInParent<CameraShake>();
    }

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        playerLastKnownPosition = playerTarget.position;

        startingPosition = transform.position;

        lastFollowTime = Time.time;
        turningTimeDelay = ((float)1f - (float)xSpeed);
        turningTimeDelay += 1f * turningDelayRate;

        enemyHealth = GetComponent<CharacterHealth>();
        _enemyBatch = GetComponentInParent<EnemyBatchHandler>();
        _collider2D = GetComponents<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void OnDisable()
    {
        if (!enemyHealth.IsAlive())
        {
            _enemyBatch.RemoveEnemy(this);
        }

    }

    private void Update()
    {
        if (!playerTarget || !enemyHealth.IsAlive())
        {
            return;
        }
        HandleFacingDirection();
    }

    private void FixedUpdate()
    {
        if (!enemyHealth.IsAlive())
        {
            for (int i = 0; i < _collider2D.Length; i++)
            {
                _collider2D[i].enabled = false;
            }
        }

        HandleChasingPlayer();
    }

    void HandleChasingPlayer()
    {
        if (HasPlayerTarget)
        {
            if (!dealthDamageToPlayer)
            {
                ChasePlayer();
            }
            else
            {
                if (Time.time < damageCoolDownTimer)
                {
                    enemyMovementMotion = startingPosition - transform.position;
                }
                else
                {
                    dealthDamageToPlayer = false;
                }
            }
        }

        else
        {
            enemyMovementMotion = startingPosition - transform.position;

            if (Vector3.Distance(transform.position, startingPosition) < 0.2f)
            {
                enemyMovementMotion = Vector3.zero;
            }
        }

        HandleMovement(enemyMovementMotion.x, enemyMovementMotion.y);
    }
    void ChasePlayer()
    {
        if (Time.time - lastFollowTime > turningTimeDelay)
        {
            if (playerTarget == null)
            {
                return;
            }
            else
            {
                playerLastKnownPosition = playerTarget.position;
                lastFollowTime = Time.time;
            }
        }

        if (Vector3.Distance(transform.position, playerLastKnownPosition) > 0.016f)
        {
            enemyMovementMotion = (playerLastKnownPosition - transform.position).normalized * chaseSpeed;
        }

        else
        {
            enemyMovementMotion = Vector3.zero;
        }
    }


    void HandleFacingDirection()
    {
        myScale = transform.localScale;
        if (HasPlayerTarget)
        {
            if (playerTarget.transform.position.x > transform.position.x)
            {
                myScale.x = Mathf.Abs(myScale.x);
            }
            else if (playerTarget.transform.position.x < transform.position.x)
            {
                myScale.x = -Mathf.Abs(myScale.x);
            }

        }
        else
        {
            if (startingPosition.x > transform.position.x)
            {
                myScale.x = Mathf.Abs(myScale.x);
            }
            else if (startingPosition.x < transform.position.x)
            {
                myScale.x = -Mathf.Abs(myScale.x);
            }
        }
        transform.localScale = myScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            damageCoolDownTimer = Time.time + damageCoolDownThreshold;
            dealthDamageToPlayer = true;
            col.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
            _cameraShake.ShakeCamera(enemyShakeIntensity, enemyShakeDuration);
        }
    }
}
