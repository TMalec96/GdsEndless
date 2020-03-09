﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadMenuSceneFromPauseMenu()
    {
        FindObjectOfType<GameSession>().Pause();
        SceneManager.LoadScene(0);

    }
    public void LoadGameScene()
    {
        
        SceneManager.LoadScene(1);
      

    }
    public void LoadAchievmentsScene()
    {

        SceneManager.LoadScene(3);


    }
    public void LoadATutorialScene()
    {

        SceneManager.LoadScene(4);


    }

    public void ResetGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(1);
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
