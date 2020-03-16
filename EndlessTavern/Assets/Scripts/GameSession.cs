using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    private int goodOrders = 0;
    private int badOrders = 0;
    private bool paused = false;
    private bool gameFinished = false;
    [SerializeField]
    float timer = 17;
    float timerOnStart;
    [SerializeField]
    Achievement scoreAchievement;
    [SerializeField]
    Achievement timeAchievement;
    [SerializeField]
    Achievement ordersAchievement;

    [Header("Speed Progression")]
    [SerializeField]
    int firstInterwal = 10;
    [SerializeField]
    int secondInterwal = 20;
    [SerializeField]
    int thirdInterwal = 30;

    [Header("Score Multiplayer")]
    [SerializeField]
    int dishMultiply = 3;
    [SerializeField]
    float scoreMultiplayerBase = 1f;
    [SerializeField]
    float scoreMultiplayer = 0.2f;




    private float globalTime = 0;
    private int goodOrdersCombo = 0;
    
    

    public int BadOrders { get => badOrders; set => badOrders = value; }
    public int GoodOrders { get => goodOrders; set => goodOrders = value; }
    public int Score { get => score; set => score = value; }
    public float Timer { get => timer;
        set
        {
            if (value+timer > 30)
            {
                timer = 30;
            }
            else
            {
                timer = value;
            
            }
        }
    }
    

    // Start is called before the first frame update
    private void Awake()
    {
        timerOnStart = timer;
        SetUpSingleton();
    }
    public void substractTime( int timeToSubstract)
    {
        timer -= timeToSubstract;
    }

    public void GrantBonusPointsForGoodOrdersInRow()
    {
        if(goodOrdersCombo%5 == 0)
        {
            score += 500;
        }
        if (goodOrdersCombo % 10 == 0)
        {
            score += 2500;
        }

    }
    private void Update()
    {
        globalTime += Time.deltaTime;
        checkForTimeAchievementCompletion();
        if (timer > 0)
            timer -= Time.deltaTime;
        else if (!gameFinished && (int)timer <= 0)
        {
            gameFinished = true;
            FindObjectOfType<LevelLoader>().LoadGameOverScene();
            
        }
            
     
    }
    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int value)
    {
        score += Convert.ToInt32(value*scoreMultiplayerBase);
        checkForScoreAchievementCompletion();
    }
    public void AddToGoodOrders(int value)
    {
        goodOrdersCombo += 1;
        goodOrders += value;
        if(goodOrders%dishMultiply == 0)
        {
            scoreMultiplayerBase += scoreMultiplayer;
        }
        checkForOrdersAchievementCompletion();
        GrantBonusPointsForGoodOrdersInRow();
        checkForDishSpeedProgression();


    }

    private void checkForDishSpeedProgression()
    {
        if(goodOrders == firstInterwal)
        {
            FindObjectOfType<FoodSpawner>().setDishSpeed(2);
        }
        else if (goodOrders == secondInterwal)
        {
            FindObjectOfType<FoodSpawner>().setDishSpeed(3);
        }
        else if (goodOrders == thirdInterwal)
        {
            FindObjectOfType<FoodSpawner>().setDishSpeed(4);
        }

    }

    public void AddToBadOrders(int value)
    {
        goodOrdersCombo = 0;
        badOrders += value;
    }
    public void ResetGame()
    {
       Destroy(gameObject);
    }
    
    public void checkForScoreAchievementCompletion()
    {
        if (!scoreAchievement.IsComplete && score >= scoreAchievement.Score)
        {
            
            scoreAchievement.IsComplete = true;
        }
    }
    public void checkForOrdersAchievementCompletion()
    {
        if(!ordersAchievement.IsComplete && goodOrdersCombo == ordersAchievement.OrdersCombo)
        {
            ordersAchievement.IsComplete = true;
        }

    }
    public void checkForTimeAchievementCompletion()
    {
        if(!timeAchievement.IsComplete && globalTime >= timeAchievement.Time)
        {
            timeAchievement.IsComplete = true;
        }
    }



}
