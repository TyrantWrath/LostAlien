using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapons", fileName = "Weapon Info SO")]
public class PlayerWeaponSO : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private string weaponDamage;
    [SerializeField] private int weaponMaxDurabilty;
    [SerializeField] private float weaponStartingDurability;
    [SerializeField] private float weaponBulletSpeed;
    [SerializeField] private float weaponDurability;
    [SerializeField] private Sprite weaponSprite;

    public string WeaponName { get { return weaponName; } }
    public string WeaponDamage { get { return weaponDamage; } }
    public int WeaponMaxDurabilty { get { return weaponMaxDurabilty; } }
    public float WeaponStartingDurability { get { return weaponStartingDurability; } }
    public float WeaponBulletSpeed { get { return weaponBulletSpeed; } }
    public float WeaponDurability { get { return weaponDurability; } set { weaponDurability = value; } }
    public Sprite WeaponSprite { get { return weaponSprite; } }
}

