using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health;
    [SerializeField] private Slider _slider;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();

    }
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0f)
        {
            _animator.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
        }
        if (_slider != null)
        {
            SliderUI(health);
        }

    }
    private void SliderUI(float sliderValue)
    {
        _slider.value = sliderValue;
    }


    private void DestroyCharacter()
    {
        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return health > 0 ? true : false;
    }
}
