using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intractable : MonoBehaviour
{
    public WeaponIntractSO _weaponIntractSO;
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _weaponIntractSO._MySprite;
    }


}
