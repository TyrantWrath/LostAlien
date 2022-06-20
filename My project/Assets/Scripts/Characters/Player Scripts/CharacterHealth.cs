using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health;
    [SerializeField] private Slider _slider;
    public Image bossHealthUI;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _heartAnimator;
    [SerializeField] ParticleSystem _particleSystem;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

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
            _heartAnimator.SetTrigger(TagManager.PLAYER_UI_GOT_HEALED_PARAMETER);
            SliderUI();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (_heartAnimator != null)
        {
            _heartAnimator.SetTrigger(TagManager.PLAYER_UI_GOT_DAMAGED_PARAMETER);
            SliderUI();
        }

        else if (_heartAnimator == null)
        {
            _particleSystem.Play(false);
            StartCoroutine(ChangeColor());
        }
        if (bossHealthUI != null)
        {
            bossHealthUI.fillAmount = Mathf.Clamp(health, 0, health) / 199;
        }
        if (health <= 0f)
        {
            StopAllCoroutines();
            _animator.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
        }

    }
    public void PlayerParticleEffect()
    {
        _particleSystem.Play(false);
    }
    /*private void Update()
    {
        if (bossHealth!= null)
        {
            bossHealth.fillAmount = Mathf.Clamp01(health);
        }
    }*/
    IEnumerator ChangeColor()
    {
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
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
