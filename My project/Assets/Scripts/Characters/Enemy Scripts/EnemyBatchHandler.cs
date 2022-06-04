using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatchHandler : MonoBehaviour
{

    [SerializeField] private bool hasShooterEnemy;
    [SerializeField] private List<CharacterMovement> enemies;

    [SerializeField] private Transform shooterEnemyHolder;

    [SerializeField] private List<EnemyShooter> shooterEnemies;
    [SerializeField] private GameObject batchGate;

    private void Start()
    {
        foreach (Transform tr in GetComponentInChildren<Transform>())
        {
            if (tr != this)
            {
                enemies.Add(tr.GetComponent<CharacterMovement>());
            }
        }

        if (hasShooterEnemy)
        {
            foreach (Transform tr in shooterEnemyHolder.GetComponentInChildren<Transform>())
            {
                shooterEnemies.Add(tr.GetComponent<EnemyShooter>());
            }
        }
    }

    public void EnablePlayerTarget()
    {
        foreach (CharacterMovement charMovement in enemies)
        {
            charMovement.HasPlayerTarget = true;
        }
    }
    public void DisablePlayerTarget()
    {
        foreach (CharacterMovement charMovement in enemies)
        {
            charMovement.HasPlayerTarget = false;
        }
    }

    public void RemoveEnemy(CharacterMovement enemy)
    {
        enemies.Remove(enemy);
        CheckToUnlockGate();
    }

    public void RemoveShooterEnemy(EnemyShooter shooterEnemy)
    {
        if (shooterEnemies != null)
        {
            shooterEnemies.Remove(shooterEnemy);
        }
        CheckToUnlockGate();
    }

    private void CheckToUnlockGate()
    {
        if (hasShooterEnemy)
        {
            if (enemies.Count <= 0 && shooterEnemies.Count <= 0)
            {
                if (batchGate)
                {
                    UnlockTheGate();
                }
            }
        }
        else
        {
            if (enemies.Count <= 0)
            {
                if (batchGate)
                {
                    UnlockTheGate();
                }
            }
        }

    }

    private void UnlockTheGate()
    {
        batchGate.SetActive(false);

    }

}
