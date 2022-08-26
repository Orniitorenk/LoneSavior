using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    public GameObject openMenu, closeMenu;

    public void Open()
    {
        openMenu.SetActive(true);
        closeMenu.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitOnClick()
    {
        Application.Quit();
    }
}
