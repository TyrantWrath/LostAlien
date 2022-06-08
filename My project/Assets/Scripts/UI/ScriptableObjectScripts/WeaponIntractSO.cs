using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/IntractablesSO", fileName = "Weapon Intract Info")]
public class WeaponIntractSO : ScriptableObject
{
    public Sprite _mySprite;


    [HideInInspector] public int myWeaponIndex { get; set; }
    [HideInInspector] public float durabilyAmountToAdd { get; set; }



    [HideInInspector] public bool isHealthItem { get; set; }

    [HideInInspector] public float healthAmountToAdd { get; set; }

}
