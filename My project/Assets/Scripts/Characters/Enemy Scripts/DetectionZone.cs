using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private EnemyShooter _enemyShooter;


    private void Awake()
    {
        _enemyShooter = GetComponentInParent<EnemyShooter>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            _enemyShooter.SetPlayerInRange(true);
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            _enemyShooter.SetPlayerInRange(false);
        }

    }
}
