using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    private int goodOrders = 0;
    private int badOrders = 0;
    [SerializeField]
    float timer = 15;

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

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            print("koniec gry");
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
    }
    public void AddToBadOrders(int value)
    {
        badOrders += value;
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
