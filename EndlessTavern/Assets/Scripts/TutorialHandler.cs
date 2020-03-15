using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField]
    Canvas tutorialPrompt = null;

    
    public void CheckForTutorialCompletion()
    {
       if(PlayerPrefs.GetInt("TutorialStatus") == 1)
        {
            SetTutorialComplete();
            FindObjectOfType<LevelLoader>().LoadGameScene();
            
        }
        else
        {
            SetTutorialComplete();
            Instantiate(tutorialPrompt, new Vector2(0, 0), Quaternion.identity);
            
        }
    }
    
    public void SetTutorialComplete()
    {
        PlayerPrefs.SetInt("TutorialStatus", 1);
    }
    public void ResetTutorialStatus()
    {
        PlayerPrefs.SetInt("TutorialStatus", 0);
    }
}
