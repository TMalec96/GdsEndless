using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadMenuSceneNoReset()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadMenuSceneFromPauseMenu()
    {
        FindObjectOfType<PauseMenu>().Pause();
        FindObjectOfType<GameSession>().ResetGame();
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
    public void LoadHiscoreScene()
    {

        SceneManager.LoadScene(5);


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
