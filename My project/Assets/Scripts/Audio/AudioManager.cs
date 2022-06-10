using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [Header("GateAudioClip")]
    [SerializeField] AudioClip[] gateOpenings;

    [Space(25)]
    [Header("FadeOut Audio")]
    [SerializeField] AudioClip[] playeDeathFadeOut;

    [Space(25)]
    [Header("Weapons")]

    [Space(15)]
    [Header("Balster")]
    [SerializeField] AudioClip[] weaponIndex0;

    [Space(15)]
    [Header("Anti-Matter")]
    [SerializeField] AudioClip[] weaponIndex1;

    [Space(15)]
    [Header("Laser")]
    [SerializeField] AudioClip[] weaponIndex2;

    [Space(15)]
    [Header("Plasma")]
    [SerializeField] AudioClip[] weaponIndex3;

    [Space(25)]
    [Header("PickUp")]

    [Space(15)]
    [Header("Medkit")]
    [SerializeField] AudioClip[] medKitPickup;

    [Header("WeaponPickUp")]
    [SerializeField] AudioClip[] weaponPickUp;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGateOpeningAudio()
    {
        _audioSource.PlayOneShot(gateOpenings[Random.Range(0, 3)]);
    }

    public void PlayeDeathFadeOutAudio()
    {
        _audioSource.PlayOneShot(playeDeathFadeOut[Random.Range(0, 2)]);
    }

    #region 
    public void PlayBlasterWeaponAudio()
    {
        _audioSource.volume = 0.30f;
        _audioSource.PlayOneShot(weaponIndex0[Random.Range(0, 2)]);
    }
    public void PlayAntiMatterWeaponAudio()
    {
        _audioSource.volume = 0.40f;
        _audioSource.PlayOneShot(weaponIndex1[Random.Range(0, 3)]);
    }

    public void PlayLaserWeaponAudio()
    {
        _audioSource.volume = 0.30f;
        _audioSource.PlayOneShot(weaponIndex2[Random.Range(0, 2)]);
    }
    public void PlayPlasmaWeaponAudio()
    {
        _audioSource.volume = 0.05f;
        _audioSource.PlayOneShot(weaponIndex3[Random.Range(0, 5)]);
    }
    #endregion


    #region 
    public void playWeaponPickUpAudio()
    {
        _audioSource.PlayOneShot(medKitPickup[Random.Range(0, 3)]);
    }
    public void PlayMedKitPickUpAudio()
    {
        _audioSource.PlayOneShot(weaponPickUp[Random.Range(0, 2)]);
    }
    #endregion
}
