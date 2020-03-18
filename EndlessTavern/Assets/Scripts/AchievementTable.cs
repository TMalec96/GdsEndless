using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTable : MonoBehaviour
{
    [SerializeField]
    List<Achievement> achievements = new List<Achievement>();
    [SerializeField]
    Canvas mainCanvas;
    Transform descriptionsContainer;
    GameObject achivCounter;
    private List<GameObject> descriptionFieldsList = new List<GameObject>();
    private int xPostion = -5;
    private int achievementsCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        int textFieldIndex = 0;
        InitializeTextFields();
       foreach (Achievement achievement in achievements)
        {
            
            achievement.CheckForCompletion();
            if (achievement.Complete == 1)
            {
                achievement.setShaderCompletion();
                achievementsCounter++;
            }
            else
            {
                achievement.setShaderInCompletion();
            }
                //wyświetla zrobione achiv
                
                setAchivTextPosition(textFieldIndex, achievement);
                Instantiate(achievement, new Vector2(0 + xPostion, transform.position.y), Quaternion.identity);
                textFieldIndex++;
            
            xPostion += 5;
        }
        CountAchievements();
    }

    private void CountAchievements()
    {
        achivCounter = mainCanvas.transform.Find("AchivCounter").gameObject;
        achivCounter.GetComponent<Text>().text = achievementsCounter + " / 3"; 

    }

    private void setAchivTextPosition(int index, Achievement achievement)
    {
        descriptionFieldsList[index].gameObject.GetComponent<Text>().text = achievement.AchievementName;
        descriptionFieldsList[index].gameObject.SetActive(true);
    }
    private void InitializeTextFields()
    {
        descriptionsContainer = mainCanvas.gameObject.transform.Find("AchievmentsDescriptionsContainer");
        foreach (Transform child in descriptionsContainer.transform)
        {
            descriptionFieldsList.Add(child.gameObject);
        }

    }
    public void ResetAllAchievementsProgress()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.ResetAchievementsProgress();
            
        }
    }
    public void UnlockAllAchievementsProgress()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.UnlockAchievementsProgress();
           
        }
    }
}
