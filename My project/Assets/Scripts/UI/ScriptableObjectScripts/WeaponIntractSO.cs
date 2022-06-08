using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/IntractablesSO", fileName = "Weapon Intract Info")]
public class WeaponIntractSO : ScriptableObject
{
    public Sprite _mySprite;


    [Space(50)]
    [Header("Fill only if Weapon Item")]
    [SerializeField] public int myWeaponIndex;
    [SerializeField] private float durabilyAmountToAdd;


    [Space(50)]
    [Header("Fill only if Health Item")]
    [SerializeField] private bool isHealthItem;

    [SerializeField] private float healthAmountToAdd;

    public int MyWeaponIndex { get { return myWeaponIndex; } }
    public float DurabilyAmountToAdd { get { return durabilyAmountToAdd; } }
    public bool IsHealthItem { get { return isHealthItem; } }
    public float HealthAmountToAdd { get { return healthAmountToAdd; } }
}
