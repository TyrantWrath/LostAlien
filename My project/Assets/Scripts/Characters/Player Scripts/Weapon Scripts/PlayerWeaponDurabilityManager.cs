using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDurabilityManager : MonoBehaviour
{
    [SerializeField] PlayerWeaponUI _playerWeaponUI;

    public void AddWeaponDurability(float durabilityAmount, int weaponIndex)
    {

        _playerWeaponUI.playerWeaponSO[weaponIndex].WeaponDurability += durabilityAmount;
        if (_playerWeaponUI.playerWeaponSO[weaponIndex].WeaponDurability >= _playerWeaponUI.playerWeaponSO[weaponIndex].WeaponMaxDurabilty)
        {
            _playerWeaponUI.playerWeaponSO[weaponIndex].WeaponDurability = _playerWeaponUI.playerWeaponSO[weaponIndex].WeaponMaxDurabilty;
        }
    }
    public void BlasterShotCounter(int shotsFired = 0)
    {
        _playerWeaponUI.playerWeaponSO[0].WeaponDurability = _playerWeaponUI.playerWeaponSO[0].WeaponDurability;
    }
    public void AntiMatterShotCounter(int shotsFired)
    {
        _playerWeaponUI.playerWeaponSO[1].WeaponDurability = _playerWeaponUI.playerWeaponSO[1].WeaponDurability - shotsFired;
    }
    public void LaserShotCounter(int shotsFired)
    {
        _playerWeaponUI.playerWeaponSO[2].WeaponDurability = _playerWeaponUI.playerWeaponSO[2].WeaponDurability - shotsFired;
    }
    public void PlasmaShotCounter(int shotsFired)
    {
        _playerWeaponUI.playerWeaponSO[3].WeaponDurability = _playerWeaponUI.playerWeaponSO[3].WeaponDurability - shotsFired;
    }
}
