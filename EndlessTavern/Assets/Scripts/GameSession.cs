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
    [SerializeField]
    float timer = 15;
    float timerOnStart;
    [SerializeField]
    Canvas maincCanvas;
    

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
            else if(value+timer<30)
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

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else if ((int)timer == 0)
        {
            FindObjectOfType<LevelLoader>().LoadGameOverScene();
            timer = -1;
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
    }
    public void AddToGoodOrders(int value)
    {
        goodOrders += value;
        if (goodOrders == 5)
        {
            FindObjectOfType<TrayActivator>().SetActiveTraysNumber(2);
        }
        else if (goodOrders == 10)
        {
            FindObjectOfType<TrayActivator>().SetActiveTraysNumber(3);
        }
        
    }
    public void AddToBadOrders(int value)
    {
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
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
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
    


}
