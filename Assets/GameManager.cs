using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject LevelFailed_Panel;
    public static bool gameFailed = false;
    private void Start()
    {
        LevelFailed_Panel.SetActive(false);
        gameFailed = false;
    }
    private void Update()
    {
        if (gameFailed == true)
        {
            LevelFailed();
        }
    }
    public void LevelFailed()
    {
        // ...
        // grandpa dies
        // player dies 
        LevelFailed_Panel.SetActive(true);  
    }
    public void ContinueLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Retry()
    {
        GameManager.gameFailed = false;
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}