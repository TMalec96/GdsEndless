using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    public enum AchievementType {Score,Time,OrdersCombo }
    [SerializeField]
    string achievementName = "";
    [SerializeField]
    int time = 0;
    [SerializeField]
    int score = 0;
    [SerializeField]
    int ordersCombo = 0;
    [SerializeField]
    AchievementType achievementType = AchievementType.Score;

    bool isComplete = false;
    int complete = 0;

    public bool IsComplete { get => isComplete; set =>isComplete = setComplete(value); }

    private bool setComplete(bool value)
    {
        switch (achievementType)
        {
            case (AchievementType.Score):
                PlayerPrefs.SetInt("ScoreAchievement", 1);
                break;
            case (AchievementType.Time):
                PlayerPrefs.SetInt("TimeAchievement", 1);
                break;
            case (AchievementType.OrdersCombo):
                PlayerPrefs.SetInt("OrdersComboAchievement", 1);
                break;
        }
        return value;
    }

    public int Time { get => time; set => time = value; }
    public int Score { get => score; set => score = value; }
    public int OrdersCombo { get => ordersCombo; set => ordersCombo = value; }
    public int Complete { get => complete; set => complete = value; }

    public void ResetAchievementsProgress()
    {
        PlayerPrefs.SetInt("ScoreAchievement", 0);
        PlayerPrefs.SetInt("TimeAchievement", 0);
        PlayerPrefs.SetInt("OrdersComboAchievement", 0);
    }
    public void CheckForCompletion()
    {
        switch (achievementType)
        {
            case (AchievementType.Score):
                complete = PlayerPrefs.GetInt("ScoreAchievement");
                break;
            case (AchievementType.Time):
                complete = PlayerPrefs.GetInt("TimeAchievement");
                break;
            case (AchievementType.OrdersCombo):
                complete = PlayerPrefs.GetInt("OrdersComboAchievement");
                break;
        }
    }
}
