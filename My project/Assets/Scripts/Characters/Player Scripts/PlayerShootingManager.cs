using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private float shootingTimerLimit = 0.2f;
    private float shootingTimer;

    [SerializeField] Transform bulletSpawnPos;

    private Animator shootingAnimation;
    private CharacterHealth _characterHealth;

    private PlayerWeaponManager playerWeaponManager;
    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        shootingAnimation = bulletSpawnPos.GetComponent<Animator>();
        _characterHealth = GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (!_characterHealth.IsAlive())
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > shootingTimer)
                {
                    shootingTimer = Time.time + shootingTimerLimit;
                    shootingAnimation.SetTrigger(TagManager.SHOOT_ANIMATION_PARAMETER);

                    CreateBullet();
                }
            }
        }

    }

    void CreateBullet()
    {
        playerWeaponManager.Shoot(bulletSpawnPos.position);

    }

}
