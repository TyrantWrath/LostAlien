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
    [SerializeField] private GameObject _gateGameObject;
    [SerializeField] private GameObject bossHealthUI;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag(TagManager.AUDIO_MANAGER_TAG).GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (bossZoneDetection)
        {
            if (col.CompareTag(TagManager.PLAYER_TAG))
            {
                if (_enemyTargetType == EnemyTargetType.EnableEnemyTarget && _bossMovement)
                {
                    _bossMovement.PlayerDetectedInfo(true);
                    if (_gateGameObject != null && bossHealthUI != null && _gateGameObject.activeSelf == false)
                    {
                        _gateGameObject.SetActive(true);
                        bossHealthUI.SetActive(true);
                        _audioManager.PlayGateClosingAudio();
                    }


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
                    if (_gateGameObject != null && _gateGameObject.activeSelf == false && _enemyBatchHandler.canLockGate)
                    {
                        _gateGameObject.SetActive(true);
                        _audioManager.PlayGateClosingAudio();
                    }

                }
                else
                {
                    _enemyBatchHandler.DisablePlayerTarget();
                }
            }
        }


    }
}
