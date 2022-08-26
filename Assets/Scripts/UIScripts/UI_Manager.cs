using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : Singleton<UI_Manager>
{
    private static UI_Manager instance = null;

    [Header("Booleans")]
    public bool gameIsPaused;
    public bool opened;
    public bool gameEnded;

    [Header("UI Elements")]
    [SerializeField] private GameObject upgradePanel;
    public GameObject[] firstList, secondList, thirdList;
    public GameObject restartMenu, escMenu, endGameMenu;


    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver && !escMenu.activeSelf)
        {
            StartCoroutine(OpenRestartMenu());
        }

        OpenESCMenu();

        if (gameEnded)
        {
            Time.timeScale = 0;
            OpenPanelOfEndGame();            
        }
        
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quitting()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        if (!upgradePanel.activeSelf)
        {
            Time.timeScale = 1;
            GameManager.Instance.gameIsPaused = false;
        }
        
    }

    public void OpenPanelOfEndGame()
    {
        restartMenu.gameObject.SetActive(false);
        escMenu.gameObject.SetActive(false);

        if (gameEnded)
        {
            endGameMenu.gameObject.SetActive(true);
        }
    }


    IEnumerator OpenRestartMenu()
    {
        yield return new WaitForSeconds(1.5f);
        restartMenu.gameObject.SetActive(true);
    }

    public void ReturnMainMenu() // button listener
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }


    public void OpenESCMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escMenu.gameObject.SetActive(true);
            GameManager.Instance.gameIsPaused = true;
            Time.timeScale = 0;

            if (restartMenu.activeSelf)
            {
                restartMenu.gameObject.SetActive(false);
            }            
        }
    }

    public void OpenUpgradePanel()
    {
        // Pick a random upgrade from the bag, and show the player
        upgradePanel.gameObject.SetActive(true);
        int selection = Random.Range(0, firstList.Length);
        if (firstList[selection] != null)
        {
            firstList[selection].gameObject.SetActive(true);
        }
        else
        {
            int secondSelection = Random.Range(0, firstList.Length);
            if (secondSelection != selection)
            {
                firstList[secondSelection].gameObject.SetActive(true);
            }

        }
        int selection1 = Random.Range(0, secondList.Length);
        if (secondList[selection1] != null)
        {
            secondList[selection1].gameObject.SetActive(true);
        }
        else
        {
            int secondSelection = Random.Range(0, secondList.Length);
            if (secondSelection != selection)
            {
                secondList[secondSelection].gameObject.SetActive(true);
            }

        }
        int selection2 = Random.Range(0, thirdList.Length);
        if (thirdList[selection2] != null)
        {
            thirdList[selection2].gameObject.SetActive(true);
        }
        else
        {
            int secondSelection = Random.Range(0, thirdList.Length);
            if (secondSelection != selection)
            {
                thirdList[secondSelection].gameObject.SetActive(true);
            }

        }

        opened = true;
        Time.timeScale = 0;

    }
}
