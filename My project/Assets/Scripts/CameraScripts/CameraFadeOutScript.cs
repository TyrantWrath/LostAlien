using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFadeOutScript : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    [SerializeField] private float speedScale = 1f;
    [SerializeField] private Color fadeColor = Color.black;
    [SerializeField] private string sceneNameToLoad;

    [SerializeField]
    private AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    [SerializeField] private bool startFadedOut = false;


    private float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float alphaTimer = 0f;

    private void Start()
    {
        if (startFadedOut)
        {
            alpha = 1f;
        }
        else
        {
            alpha = 0f;
        }

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
        CameraFadeOutConditions(true, false);
    }

    public void CameraFadeOutConditions(bool startFadingOut, bool timeLoadNextScene = false)
    {
        if (direction == 0 && startFadingOut)
        {
            if (alpha >= 1f)
            {
                alpha = 1f;
                alphaTimer = 0f;
                direction = 1;
            }
            else
            {
                alpha = 0f;
                alphaTimer = 1f;
                direction = -1;
                startFadedOut = false;
            }
        }
        if (timeLoadNextScene)
        {
            Invoke("LoadTheNextScene", 2f);
        }
    }
    private void LoadTheNextScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }


    public void OnGUI()
    {
        if (alpha > 0.0f)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        }

        if (direction != 0)
        {
            alphaTimer += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(alphaTimer);

            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();

            if (alpha <= 0f || alpha >= 1f)
            {
                direction = 0;
            }
        }
    }
}
