using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float moveSpeed = 4.5f;
    [SerializeField] private float damageAmount;

    private bool dealtDamage;

    [SerializeField] float deactivateTimer = 3f;
    [SerializeField] bool destroyObj;
    PlayerWeaponManager _playerWeaponManager;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Sprite _bulletSprite;
    private Color initialColor;
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _bulletSprite = _spriteRenderer.sprite;
        _playerWeaponManager = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerWeaponManager>();
    }

    private void OnEnable()
    {
        _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, false);
        _animator.enabled = false;

        dealtDamage = false;
        Invoke("DeactivateBullet", deactivateTimer);
        _spriteRenderer.sprite = _bulletSprite;
    }
    private void OnDisable()
    {
        _rigidBody2D.velocity = Vector2.zero;
    }

    public void MoveInDirection(Vector3 direction)
    {
        _rigidBody2D.velocity = direction * moveSpeed;
    }
    void DeativateBullet()
    {
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.ENEMY_TAG) ||
            col.CompareTag(TagManager.SHOOTER_ENEMY_TAG) ||
            col.CompareTag(TagManager.BOSS_TAG))
        {
            CancelInvoke("DeactivateBullet");
            _animator.enabled = true;
            _rigidBody2D.velocity = Vector2.zero;
            _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);

            #region 
            if (!dealtDamage)
            {
                damageAmount = 10;
                dealtDamage = true;
                col.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
            }
            #endregion
        }


        if (col.CompareTag(TagManager.BLOCKING_TAG) || col.CompareTag(TagManager.ENEMY_BULLET_TAG))
        {
            CancelInvoke("DeactivateBullet");
            _animator.enabled = true;
            _rigidBody2D.velocity = Vector2.zero;
            _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagManager.BARRIER_LAYER_TAG))
        {
            _rigidBody2D.velocity = Vector2.zero;
            CancelInvoke("DeactivateBullet");
            _animator.enabled = true;
            _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
            col.GetComponent<Animator>().SetTrigger(TagManager.BULLET_EXIT_TIGGER_PARAMETER);
        }
    }



    /* public void BulletReachedBarrier(bool bulletHitBarrier)
     {
         _rigidBody2D.velocity = Vector2.zero;
         CancelInvoke("DeactivateBullet");
         _animator.enabled = true;
         _animator.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
     }
 */
}
