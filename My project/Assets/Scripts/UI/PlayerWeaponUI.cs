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
            playerWeaponSO[i].weaponDurability = 50;
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
        //Debug.Log(_currentWeaponInUse);
        if (weaponIndex == 0)
        {
            _weaponImage.sprite = playerWeaponSO[0].weaponSprite;
            _weaponNameText.text = playerWeaponSO[0].weaponName;
            _weaponDamageText.text = playerWeaponSO[0].weaponDamage;
            _weaponDurabilitySlider.value = playerWeaponSO[0].weaponDurability;

        }
        else if (weaponIndex == 1)
        {
            _weaponImage.sprite = playerWeaponSO[1].weaponSprite;
            _weaponNameText.text = playerWeaponSO[1].weaponName;
            _weaponDamageText.text = playerWeaponSO[1].weaponDamage;
            _weaponDurabilitySlider.value = playerWeaponSO[1].weaponDurability;

        }
        else if (weaponIndex == 2)
        {
            _weaponImage.sprite = playerWeaponSO[2].weaponSprite;
            _weaponNameText.text = playerWeaponSO[2].weaponName;
            _weaponDamageText.text = playerWeaponSO[2].weaponDamage;
            _weaponDurabilitySlider.value = playerWeaponSO[2].weaponDurability;

        }
        else if (weaponIndex == 3)
        {
            _weaponImage.sprite = playerWeaponSO[3].weaponSprite;
            _weaponNameText.text = playerWeaponSO[3].weaponName;
            _weaponDamageText.text = playerWeaponSO[3].weaponDamage;
            _weaponDurabilitySlider.value = playerWeaponSO[3].weaponDurability;

        }
    }
}
