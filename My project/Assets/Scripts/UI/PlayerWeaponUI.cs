using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponUI : MonoBehaviour
{
    [SerializeField] public PlayerWeaponSO[] playerWeaponSO;
    [SerializeField] private Image _weaponImage;
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] private TextMeshProUGUI _weaponDamageText;
    [SerializeField] private Slider _weaponDurabilitySlider;


    private void Awake()
    {
        for (int i = 0; i < playerWeaponSO.Length; i++)
        {
            playerWeaponSO[i].WeaponDurability = playerWeaponSO[i].WeaponStartingDurability;
        }
    }
    void Start()
    {
        if (playerWeaponSO == null ||
        _weaponImage == null ||
        _weaponNameText == null ||
         _weaponDamageText == null ||
          _weaponDurabilitySlider == null)
        {
            Debug.Log("WeaponUI is missing something");
            return;
        }

    }

    // Update is called once per frame
    public void UpdateWeaponUI(int weaponIndex = 0)
    {
        switch (weaponIndex)
        {
            case 0:
                _weaponImage.sprite = playerWeaponSO[0].WeaponSprite;
                _weaponNameText.text = playerWeaponSO[0].WeaponName;
                _weaponDamageText.text = playerWeaponSO[0].WeaponDamage;
                _weaponDurabilitySlider.value = playerWeaponSO[0].WeaponDurability;
                break;


            case 1:
                _weaponImage.sprite = playerWeaponSO[1].WeaponSprite;
                _weaponNameText.text = playerWeaponSO[1].WeaponName;
                _weaponDamageText.text = playerWeaponSO[1].WeaponDamage;
                _weaponDurabilitySlider.value = playerWeaponSO[1].WeaponDurability;
                break;


            case 2:
                _weaponImage.sprite = playerWeaponSO[2].WeaponSprite;
                _weaponNameText.text = playerWeaponSO[2].WeaponName;
                _weaponDamageText.text = playerWeaponSO[2].WeaponDamage;
                _weaponDurabilitySlider.value = playerWeaponSO[2].WeaponDurability;
                break;


            case 3:
                _weaponImage.sprite = playerWeaponSO[3].WeaponSprite;
                _weaponNameText.text = playerWeaponSO[3].WeaponName;
                _weaponDamageText.text = playerWeaponSO[3].WeaponDamage;
                _weaponDurabilitySlider.value = playerWeaponSO[3].WeaponDurability;
                break;

        }

    }
}
