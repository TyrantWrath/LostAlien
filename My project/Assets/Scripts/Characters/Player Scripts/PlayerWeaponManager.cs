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

    [Space(25)]
    [Header("WeaponBulletList")]
    [SerializeField] ParticleSystem[] _particleSystems;

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
    private AudioManager _audioManager;

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

        _audioManager = GameObject.FindGameObjectWithTag(TagManager.AUDIO_MANAGER_TAG).GetComponent<AudioManager>();
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
        else if (_playerWeaponUI.playerWeaponSO[1].WeaponDurability <= 0 &&
        _playerWeaponUI.playerWeaponSO[2].WeaponDurability <= 0 &&
        _playerWeaponUI.playerWeaponSO[3].WeaponDurability <= 0)
        {
            if (weaponIndex != 0)
            {
                SwitchToNextWeapon();
            }
            return;
        }
        else
        {
            if (_playerWeaponUI.playerWeaponSO[weaponIndex].WeaponDurability <= 0)
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
        switch (weaponIndex)
        {

            case 0:
                _cameraShake.ShakeCamera(blasterCameraShake, shakeDuration);
                _playerWeaponDurabilityManager.BlasterShotCounter();
                _audioManager.PlayBlasterWeaponAudio();
                _particleSystems[0].Play(false);
                break;

            case 1:
                _cameraShake.ShakeCamera(matterCameraShake, shakeDuration);
                _playerWeaponDurabilityManager.AntiMatterShotCounter(1);
                _audioManager.PlayAntiMatterWeaponAudio();
                _particleSystems[1].Play(false);
                break;


            case 2:
                _cameraShake.ShakeCamera(laserCameraShake, shakeDuration);
                _playerWeaponDurabilityManager.LaserShotCounter(1);
                _audioManager.PlayLaserWeaponAudio();
                _particleSystems[2].Play(false);
                break;


            case 3:
                _cameraShake.ShakeCamera(plasmaCameraShake, shakeDuration);
                _playerWeaponDurabilityManager.PlasmaShotCounter(1);
                _audioManager.PlayPlasmaWeaponAudio();
                _particleSystems[3].Play(false);
                break;
        }
    }
}
