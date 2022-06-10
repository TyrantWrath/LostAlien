using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/IntractablesSO", fileName = "Weapon Intract Info")]
public class WeaponIntractSO : ScriptableObject
{
    [SerializeField] private Sprite _mySprite;


    [SerializeField] private int myWeaponIndex;
    [SerializeField] private float durabilyAmountToAdd;



    [SerializeField] private bool isHealthItem;

    [SerializeField] private float healthAmountToAdd;


    public Sprite _MySprite { get { return _mySprite; } }
    public int MyWeaponIndex { get { return myWeaponIndex; } }
    public float DurabilityAmountTOAdd { get { return durabilyAmountToAdd; } }
    public bool IsHealthItem { get { return isHealthItem; } }
    public float HealthAmountToAdd { get { return healthAmountToAdd; } }

}
