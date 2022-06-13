using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float minSpeed = 0.7f;
    [SerializeField] private float maxSpeed = 1.3f;
    private float speed;
    [SerializeField] private float maxRotationSpeed = 330f;
    [SerializeField] private float minRotationSpeed = 220f;
    private float rotationValue;

    [SerializeField] private float damageAmount = 25f;

    [SerializeField] private float enemyBulletShakeIntensity = 1f;
    [SerializeField] private float enemyBulletShakeDuration = 0.3f;
    [SerializeField] private float deactivateTimer = 5f;

    private bool dealthDamage;

    Rigidbody2D _rigidBody2D;
    Animator _animator;
    private CameraShake _cameraShake;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        if (target == null)
        {
            return;
        }

        _cameraShake = Camera.main.GetComponentInParent<CameraShake>();

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, false);
        Invoke("DeactiveMissile", deactivateTimer);


        rotationValue = Random.Range(minRotationSpeed, maxRotationSpeed);
        speed = Random.Range(minSpeed, maxSpeed);
    }
    private void DeativateBullet()
    {

        DeactiveMissile();
    }

    private void DeactiveMissile()
    {
        _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
        _cameraShake.ShakeCamera(enemyBulletShakeIntensity, enemyBulletShakeDuration);

        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector2 dir = (Vector2)target.position - _rigidBody2D.position;

        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, transform.up).z;

        _rigidBody2D.angularVelocity = -rotateAmount * rotationValue;
        _rigidBody2D.velocity = transform.up * speed;

    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag(TagManager.BLOCKING_TAG) || col.CompareTag(TagManager.BULLET_TAG))
        {
            CancelInvoke("DeactiveMissile");

            _rigidBody2D.velocity = Vector3.zero;
            _animator.SetTrigger(TagManager.EXPLODE_ANIMATION_PARAMETER);
        }

        else if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            CancelInvoke("DeactiveMissile");

            _rigidBody2D.velocity = Vector3.zero;
            _animator.SetTrigger(TagManager.EXPLODE_ANIMATION_PARAMETER);

            if (!dealthDamage)
            {
                dealthDamage = true;

                col.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
                col.GetComponent<CharacterHealth>().PlayerParticleEffect();

                _cameraShake.ShakeCamera(enemyBulletShakeIntensity, enemyBulletShakeDuration);
            }
        }
    }
}
