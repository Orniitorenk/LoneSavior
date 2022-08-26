using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameManager instance = null;

    [Header("Game States Check")]
    public bool isGameOver = false;
    public bool upgradeState = false;
    public bool gameIsPaused = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
