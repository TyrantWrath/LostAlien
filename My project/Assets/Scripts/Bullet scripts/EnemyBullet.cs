using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private bool isSlow, canRotate;
    [SerializeField] private bool poolBullet;
    [SerializeField] private float deactiveTimer = 5;

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;

    private CameraShake _cameraShake;

    [SerializeField] private float enemyBulletShakeIntensity = 0.6f;
    [SerializeField] private float enemyBulletShakeDuration = 0.2f;
    [SerializeField] private float damageAmount = 10f;
    private bool dealthDamage;



    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _cameraShake = Camera.main.GetComponentInParent<CameraShake>();
    }

    private void Start()
    {
        Invoke("DeativeBullet", deactiveTimer);
    }

    private void FixedUpdate()
    {
        if (isSlow)
        {
            _rigidBody2D.velocity = Vector2.Lerp(_rigidBody2D.velocity, Vector2.zero, Random.value * Time.deltaTime);
        }

        if (canRotate)
        {
            transform.Rotate(Vector3.forward * 60f);
        }
    }
    private void onDisable()
    {
        transform.rotation = Quaternion.identity;
        isSlow = false;
    }

    void DeativeBullet()
    {

        if (poolBullet)
        {
            gameObject.SetActive(false);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void SetIsSlow(bool slow)
    {
        isSlow = slow;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.BLOCKING_TAG) || col.CompareTag(TagManager.BULLET_TAG))
        {
            _rigidBody2D.velocity = Vector2.zero;
            CancelInvoke("DeativeBullet");
            _animator.SetTrigger(TagManager.EXPLODE_ANIMATION_PARAMETER);
        }

        else if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            _rigidBody2D.velocity = Vector2.zero;
            CancelInvoke("DeativeBullet");
            _animator.SetTrigger(TagManager.EXPLODE_ANIMATION_PARAMETER);

            if (!dealthDamage)
            {
                dealthDamage = true;
                col.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
                _cameraShake.ShakeCamera(enemyBulletShakeIntensity, enemyBulletShakeDuration);
            }
        }
    }


}
