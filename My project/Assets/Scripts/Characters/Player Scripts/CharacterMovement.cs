using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float xSpeed = 1f, ySpeed = 0.75f;
    private Vector3 moveDelta;
    private RaycastHit2D movementHit;
    private BoxCollider2D _boxColllider2D;
    private bool _hasPlayerTarget;
    public bool HasPlayerTarget
    {
        get { return _hasPlayerTarget; }
        set { _hasPlayerTarget = value; }
    }
    protected virtual void Awake()
    {
        _boxColllider2D = GetComponent<BoxCollider2D>();
    }
    protected virtual void HandleMovement(float x, float y)
    {
        moveDelta = new Vector3(x * xSpeed, y * ySpeed, 0f);

        movementHit = Physics2D.BoxCast(transform.position, _boxColllider2D.size, 0f,
         new Vector2(0f, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime),
         LayerMask.GetMask(TagManager.BLOCKING_LAYER_MASK));
        if (movementHit == false)
        {
            transform.Translate(0f, moveDelta.y * Time.deltaTime, 0f);
        }

        movementHit = Physics2D.BoxCast(transform.position, _boxColllider2D.size, 0f,
         new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime),
         LayerMask.GetMask(TagManager.BLOCKING_LAYER_MASK));
        if (movementHit == false)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0f, 0f);
        }
    }
    public Vector2 GetMoveDelta()
    {
        return moveDelta;
    }
}
