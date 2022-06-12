using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image bossHealth;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _heartAnimator;

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
            StartCoroutine(ChangeColor());
        }
        if (bossHealth != null)
        {
            bossHealth.fillAmount = Mathf.Clamp(health, 0, health) / 199;
            Debug.Log(bossHealth.fillAmount);
        }
        if (health <= 0f)
        {
            StopAllCoroutines();
            _animator.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
        }

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
