using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private GameObject pressEButton;

    [SerializeField] private GameObject[] lootArray;
    private int theLootIndex;
    private bool chestCanSpawnLoot;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        pressEButton.SetActive(false);

        for (int i = 0; i < lootArray.Length; i++)
        {
            lootArray[i].SetActive(false);
        }

        theLootIndex = Random.Range(0, lootArray.Length);

    }
    private void Update()
    {
        if (chestCanSpawnLoot)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetTrigger(TagManager.OPEN_CHEST_ANIM_PARAMETER);

                lootArray[theLootIndex].SetActive(true);
                chestCanSpawnLoot = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
            pressEButton.SetActive(true);

        }
        else
        {
            pressEButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            chestCanSpawnLoot = true;
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            chestCanSpawnLoot = false;
        }
    }
}
