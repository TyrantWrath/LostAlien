using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private CharacterMovement enemyMovement;
    private BoxCollider2D _boxCollider2D;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemyMovement = GetComponent<CharacterMovement>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        enemyMovementAnimation();
    }

    void enemyMovementAnimation()
    {
        if (enemyMovement.GetMoveDelta().magnitude > 0.0f)
        {
            _animator.SetBool(TagManager.WALK_ANIMATION_PARAMETER, true);
        }
        else
        {
            _animator.SetBool(TagManager.WALK_ANIMATION_PARAMETER, false);
        }

    }
    private void DeathAnimation()
    {
        _boxCollider2D.enabled = false;
        _animator.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);

    }

}
