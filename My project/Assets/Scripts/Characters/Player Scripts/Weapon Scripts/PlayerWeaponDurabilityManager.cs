using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDurabilityManager : MonoBehaviour
{
    [SerializeField] PlayerWeaponUI _playerWeaponUI;

    public void BlasterShotCounter(int shotsFired = 0)
    {
        _playerWeaponUI.playerWeaponSO[0].weaponDurability = _playerWeaponUI.playerWeaponSO[0].weaponDurability;
    }
    public void AntiMatterShotCounter(int shotsFired)
    {
        _playerWeaponUI.playerWeaponSO[1].weaponDurability = _playerWeaponUI.playerWeaponSO[1].weaponDurability - shotsFired;
    }
    public void LaserShotCounter(int shotsFired)
    {
        _playerWeaponUI.playerWeaponSO[2].weaponDurability = _playerWeaponUI.playerWeaponSO[2].weaponDurability - shotsFired;
    }
    public void PlasmaShotCounter(int shotsFired)
    {
        _playerWeaponUI.playerWeaponSO[3].weaponDurability = _playerWeaponUI.playerWeaponSO[3].weaponDurability - shotsFired;
    }
}
