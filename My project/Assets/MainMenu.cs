using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _selectLevelUI;

    private void Awake()
    {
        _selectLevelUI.SetActive(false);
        _mainMenu.SetActive(true);
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
