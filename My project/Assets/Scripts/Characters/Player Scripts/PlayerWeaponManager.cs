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

    [SerializeField] float blasterCameraShake;
    [SerializeField] float laserCameraShake;
    [SerializeField] float matterCameraShake;
    [SerializeField] float plasmaCameraShake;

    private Vector2 targetPos;
    private Vector2 direction;
    private Camera mainCam;
    private Vector2 bulletSpawnPosition;
    private Quaternion bulletRotation;

    private CameraShake _cameraShake;
    private CharacterHealth playerHealth;
    private PlayerWeaponDurabilityManager _playerWeaponDurabilityManager;
    private PlayerWeaponUI _playerWeaponUI;

    [SerializeField] private float shakeDuration = 0.2f;
    private void Awake()
    {
        weaponIndex = 0;
        playerWeapons[weaponIndex].gameObject.SetActive(true);
        mainCam = Camera.main;
        _cameraShake = mainCam.GetComponentInParent<CameraShake>();
        playerHealth = GetComponent<CharacterHealth>();
        _playerWeaponDurabilityManager = GetComponent<PlayerWeaponDurabilityManager>();
        _playerWeaponUI = GameObject.FindObjectOfType<Canvas>().GetComponent<PlayerWeaponUI>();
    }

    private void Update()
    {
        ChangeWeapon();
        _playerWeaponUI.UpdateWeaponUI(weaponIndex);
    }

    public void ActivateGun(int gunIndex)
    {
        ResetWeaponIndex();
        playerWeapons[weaponIndex].ActivateGun(gunIndex);
    }
    void ChangeWeapon()
    {
        if (!playerHealth.IsAlive())
        {
            return;
        }
        else if (_playerWeaponUI.playerWeaponSO[1].weaponDurability <= 0 &&
        _playerWeaponUI.playerWeaponSO[2].weaponDurability <= 0 &&
        _playerWeaponUI.playerWeaponSO[3].weaponDurability <= 0)
        {
            if (weaponIndex != 0)
            {
                SwitchToNextWeapon();
            }
            return;
        }
        else
        {
            if (_playerWeaponUI.playerWeaponSO[weaponIndex].weaponDurability <= 0)
            {
                SwitchToNextWeapon();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchToNextWeapon();
            }
        }
    }
    private void SwitchToNextWeapon()
    {
        playerWeapons[weaponIndex].gameObject.SetActive(false);
        weaponIndex++;

        ResetWeaponIndex();
        playerWeapons[weaponIndex].gameObject.SetActive(true);
    }
    private void ResetWeaponIndex()
    {
        if (weaponIndex == playerWeapons.Length)
        {
            weaponIndex = 0;
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
            _cameraShake.ShakeCamera(blasterCameraShake, shakeDuration);
            _playerWeaponDurabilityManager.BlasterShotCounter();
        }
        else if (weaponIndex == 1)
        {
            _cameraShake.ShakeCamera(matterCameraShake, shakeDuration);

            _playerWeaponDurabilityManager.AntiMatterShotCounter(1);
        }
        else if (weaponIndex == 2)
        {
            _cameraShake.ShakeCamera(laserCameraShake, shakeDuration);

            _playerWeaponDurabilityManager.LaserShotCounter(1);
        }
        else if (weaponIndex == 3)
        {
            _cameraShake.ShakeCamera(plasmaCameraShake, shakeDuration);

            _playerWeaponDurabilityManager.PlasmaShotCounter(1);
        }

        //GameObject newBullet = Instantiate(weaponBullets[weaponIndex], spawnPos, bulletRotation);
        //newBullet.GetComponent<Bullet>().MoveInDirection(direction);
    }
}
