using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _selectLevelUI;
    [SerializeField] GameObject pauseMenu;
    private bool isPauseMenuActive;

    private void Awake()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            isPauseMenuActive = false;

        }
        else
        {
            _selectLevelUI.SetActive(false);
            _mainMenu.SetActive(true);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseMenuActive)
            {
                pauseMenu.SetActive(true);
                isPauseMenuActive = true;
                Time.timeScale = 0;
            }
            else if (isPauseMenuActive)
            {
                pauseMenu.SetActive(false);
                isPauseMenuActive = false;
                Time.timeScale = 1;
            }

        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        isPauseMenuActive = false;
        pauseMenu.SetActive(false);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

















    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SelectLevelUI()
    {
        _mainMenu.gameObject.SetActive(false);
        _selectLevelUI.gameObject.SetActive(true);
    }
    public void BackButton()
    {
        _selectLevelUI.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void ExitLevel()
    {
        Application.Quit();
    }

}
