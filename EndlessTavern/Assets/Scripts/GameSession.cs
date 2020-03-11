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
    float timer = 15;
    float timerOnStart;
    [SerializeField]
    Canvas maincCanvas = null;
    [SerializeField]
    Achievement scoreAchievement;
    [SerializeField]
    Achievement timeAchievement;
    [SerializeField]
    Achievement ordersAchievement;



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
    public bool Paused { get => paused; set => paused = value; }

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
        score += value;
        checkForScoreAchievementCompletion();
    }
    public void AddToGoodOrders(int value)
    {
        goodOrdersCombo += 1;
        goodOrders += value;
        checkForOrdersAchievementCompletion();
        GrantBonusPointsForGoodOrdersInRow();


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
    public void Pause()
    {
        

        if (Time.timeScale == 0f)
        {
            print("unpause");
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
            print("pause");
            Time.timeScale = 0f;
            paused = true;
           
        }
    }
    void OnGUI()
    {
        if (paused && maincCanvas!=null)
        {
            maincCanvas.transform.Find("BackToMenuButton").gameObject.SetActive(true);
            
        }
        else if( !paused && maincCanvas !=null)
        {
            maincCanvas.transform.Find("BackToMenuButton").gameObject.SetActive(false);
        }
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
