using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTargetType
{
    EnableEnemyTarget, DisableEnemyTarget
}

public class EnemyTargetController : MonoBehaviour
{
    [SerializeField] private EnemyTargetType _enemyTargetType;
    [SerializeField] private EnemyBatchHandler _enemyBatchHandler;

    [SerializeField] private BossMovement _bossMovement;
    [SerializeField] private bool bossZoneDetection;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (bossZoneDetection)
        {
            if (col.CompareTag(TagManager.PLAYER_TAG))
            {
                if (_enemyTargetType == EnemyTargetType.EnableEnemyTarget && _bossMovement)
                {
                    _bossMovement.PlayerDetectedInfo(true);
                }
                else if (_enemyTargetType == EnemyTargetType.DisableEnemyTarget && _bossMovement)
                {
                    _bossMovement.PlayerDetectedInfo(false);
                }
            }


        }
        else
        {
            if (col.CompareTag(TagManager.PLAYER_TAG))
            {
                if (_enemyTargetType == EnemyTargetType.EnableEnemyTarget)
                {
                    _enemyBatchHandler.EnablePlayerTarget();
                }
                else
                {
                    _enemyBatchHandler.DisablePlayerTarget();
                }
            }
        }


    }
}
