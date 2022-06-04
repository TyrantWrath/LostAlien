using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("PlayerWeaponList")]
    [SerializeField] private WeaponManager[] playerWeapons;
    public int weaponIndex;

    [Header("WeaponBulletList")]
    [SerializeField] GameObject[] weaponBullets;
    [SerializeField] float pistolCameraShake;
    [SerializeField] float laserCameraShake;
    [SerializeField] float matterCameraShake;
    [SerializeField] float flameCameraShake;

    private Vector2 targetPos;
    private Vector2 direction;
    private Camera mainCam;
    private Vector2 bulletSpawnPosition;
    private Quaternion bulletRotation;
    private CameraShake _cameraShake;
    private CharacterHealth playerHealth;

    [SerializeField] private float shakeDuration = 0.2f;
    private void Awake()
    {
        weaponIndex = 0;
        playerWeapons[weaponIndex].gameObject.SetActive(true);

        mainCam = Camera.main;
        _cameraShake = mainCam.GetComponentInParent<CameraShake>();
        playerHealth = GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        ChangeWeapon();
    }

    public void ActivateGun(int gunIndex)
    {
        playerWeapons[weaponIndex].ActivateGun(gunIndex);
    }
    void ChangeWeapon()
    {
        if (!playerHealth.IsAlive())
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerWeapons[weaponIndex].gameObject.SetActive(false);

                weaponIndex++;
                if (weaponIndex == playerWeapons.Length)
                {
                    weaponIndex = 0;
                }

                playerWeapons[weaponIndex].gameObject.SetActive(true);
            }
        }

    }
    public void Shoot(Vector3 spawnPos)
    {
        targetPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        bulletSpawnPosition = new Vector2(spawnPos.x, spawnPos.y);
        direction = (targetPos - bulletSpawnPosition).normalized;

        bulletRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        BulletPool.instance.FireBullet(weaponIndex, spawnPos, bulletRotation, direction);

        if (weaponIndex == 0)
        {
            _cameraShake.ShakeCamera(pistolCameraShake, shakeDuration);
        }
        else if (weaponIndex == 1)
        {
            _cameraShake.ShakeCamera(laserCameraShake, shakeDuration);
        }
        else if (weaponIndex == 2)
        {
            _cameraShake.ShakeCamera(matterCameraShake, shakeDuration);
        }
        else if (weaponIndex == 3)
        {
            _cameraShake.ShakeCamera(flameCameraShake, shakeDuration);
        }

        //GameObject newBullet = Instantiate(weaponBullets[weaponIndex], spawnPos, bulletRotation);
        //newBullet.GetComponent<Bullet>().MoveInDirection(direction);
    }
}
