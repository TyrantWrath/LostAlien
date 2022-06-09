using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyShooterType
{
    Horizontal,
    Vertical,
    Stationary
}

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyShooterType _enemyShooterType;

    private float min_XY_Pos, max_XY_Pos;
    private Vector3 minPos, maxPos;
    [SerializeField] private float changingPosition_Delay = 1f;
    private float changingPosition_Timer;


    private Vector3 startingPos;
    private Vector3 targetPos;
    private bool changedPos;


    [SerializeField] private float moveSpeed = .76f;
    private Vector3 myScale;


    private EnemyShooterController _enemyShootController;
    private bool playerInRange;
    [SerializeField] private float shootTimeDelay = 2f;
    private float shootTimer;
    private Transform playerTransform;

    [SerializeField] private Transform bulletSpawnPos;
    private CharacterHealth enemyHealth;

    [Space(20)]
    [Header("Select Enemy Group Here")]
    [SerializeField] private EnemyBatchHandler enemyBatch;

    private void Awake()
    {
        startingPos = transform.position;

        if (_enemyShooterType == EnemyShooterType.Horizontal)
        {
            min_XY_Pos = transform.GetChild(0).transform.localPosition.x;
            max_XY_Pos = transform.GetChild(1).transform.localPosition.x;
        }

        else if (_enemyShooterType == EnemyShooterType.Vertical)
        {
            min_XY_Pos = transform.GetChild(0).transform.localPosition.y;
            max_XY_Pos = transform.GetChild(1).transform.localPosition.y;
        }

        else
        {
            minPos = transform.GetChild(0).transform.position;
            maxPos = transform.GetChild(1).transform.position;
            targetPos = maxPos;
        }

        changingPosition_Timer = Time.time + changingPosition_Delay;
        _enemyShootController = GetComponent<EnemyShooterController>();
        enemyHealth = GetComponent<CharacterHealth>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }
    private void OnDisable()
    {
        if (!enemyHealth.IsAlive())
        {
            enemyBatch.RemoveShooterEnemy(this);
        }


    }

    private void Update()
    {
        if (!enemyHealth.IsAlive() || !playerTransform)
            return;

        CheckToShootPlayer();
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (_enemyShooterType == EnemyShooterType.Horizontal)
        {
            if (!changedPos)
            {
                float xPos = Random.Range(min_XY_Pos, max_XY_Pos);
                targetPos = startingPos + Vector3.right * xPos;
                changedPos = true;
            }
        }

        else if (_enemyShooterType == EnemyShooterType.Vertical)
        {
            if (!changedPos)
            {
                float yPos = Random.Range(min_XY_Pos, max_XY_Pos);
                targetPos = startingPos + Vector3.up * yPos;
                changedPos = true;
            }
        }

        else
        {
            if (!changedPos)
            {
                targetPos = maxPos == targetPos ? minPos : maxPos;
                changedPos = true;
            }
        }

        if (Vector3.Distance(transform.position, targetPos) <= 0.07f)
        {
            if (Time.time > changingPosition_Timer)
            {
                changedPos = false;
                changingPosition_Timer = Time.time + changingPosition_Delay;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        HandleFacingDirection();

    }

    private void HandleFacingDirection()
    {
        myScale = transform.localScale;

        if (targetPos.x < transform.position.x)
        {
            myScale.x = Mathf.Abs(myScale.x);
        }
        if (targetPos.x > transform.position.x)
        {
            myScale.x = -Mathf.Abs(myScale.x);
        }

        transform.localScale = myScale;

    }

    private void CheckToShootPlayer()
    {
        if (playerInRange)
        {
            if (Time.time > shootTimer)
            {
                shootTimer = Time.time + shootTimeDelay;
                Vector2 direction = (playerTransform.position - bulletSpawnPos.position).normalized;
                _enemyShootController.Shoot(direction, bulletSpawnPos.position);
            }

        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        playerInRange = inRange;
    }

}
