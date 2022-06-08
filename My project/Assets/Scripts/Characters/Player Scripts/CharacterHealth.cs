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
    [SerializeField] Animator _heartAnimator;

    void Awake()
    {
        _animator = GetComponent<Animator>();

    }
    private void Start()
    {
        health = maxHealth;
    }

    public void HealthTOADD(float healtHmount)
    {

        health += healtHmount;
        if (_heartAnimator != null)
        {
            _heartAnimator.SetTrigger(TagManager.PLAYER_UI_GOT_DAMAGED_PARAMETER);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        SliderUI();
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (_heartAnimator != null)
        {
            _heartAnimator.SetTrigger(TagManager.PLAYER_UI_GOT_DAMAGED_PARAMETER);
        }
        if (health <= 0f)
        {
            _animator.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
        }
        if (_slider != null)
        {
            SliderUI();
        }

    }
    private void SliderUI()
    {
        _slider.value = health;
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
