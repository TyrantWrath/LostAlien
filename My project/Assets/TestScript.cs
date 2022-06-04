using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] GameObject globalVolume;
    private bool isCurrentlyActive;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isCurrentlyActive)
            {
                globalVolume.SetActive(true);
                isCurrentlyActive = true;
            }
            else if (isCurrentlyActive)
            {
                globalVolume.SetActive(false);
                isCurrentlyActive = false;
            }

        }
    }
}

