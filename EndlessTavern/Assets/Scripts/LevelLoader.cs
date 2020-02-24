using System.Collections;
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
