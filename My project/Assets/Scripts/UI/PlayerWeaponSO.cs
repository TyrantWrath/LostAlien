using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons", fileName = "Weapon Info SO")]
public class PlayerWeaponSO : ScriptableObject
{
    public string weaponName;
    public string weaponDamage;
    public int weaponMaxDurabilty;
    public float weaponBulletSpeed;
    public float weaponDurability;
    public Sprite weaponSprite;
}
