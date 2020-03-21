using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
   public Animator transitionAnim;

    IEnumerator LoadScene(int sceneNumber)
    {
        
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneNumber);

    }
   public void LoadMenuScene()
    {
        StartCoroutine(LoadScene(0));
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadMenuSceneNoReset()
    {
        StartCoroutine(LoadScene(0));
    }
    public void LoadMenuSceneFromPauseMenu()
    {
        FindObjectOfType<PauseMenu>().Pause();
        FindObjectOfType<GameSession>().ResetGame();
        StartCoroutine(LoadScene(0));

    }
    public void LoadGameScene()
    {

        StartCoroutine(LoadScene(1));


    }
    public void LoadAchievmentsScene()
    {

        StartCoroutine(LoadScene(3));


    }
    public void LoadATutorialScene()
    {

        StartCoroutine(LoadScene(4));


    }
    public void LoadHiscoreScene()
    {

        StartCoroutine(LoadScene(5));


    }

    public void ResetGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        StartCoroutine(LoadScene(1));
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(LoadScene(2));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
