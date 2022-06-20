using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    CameraFadeOutScript _cameraFadeOutScript;
    private void Start()
    {
        _cameraFadeOutScript = Camera.main.GetComponent<CameraFadeOutScript>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            _cameraFadeOutScript.CameraFadeOutConditions(true, true);
        }
    }
}
