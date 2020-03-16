using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderDishSpawner : MonoBehaviour
{
    [Header("Score")]
    [SerializeField]
    int scoreValue = 100;
    [SerializeField]
    float maxTimeScoreMultiplayer = 1f;
    [SerializeField]
    float midTimeScoreMultiplayer = 0.75f;
    [SerializeField]
    float minTimeScoreMultiplayer = 0.5f;
    [Header("Dishes Count")]
    [SerializeField]
    int maxAmountOfGoodOrdersForOneDish = 5;
    [SerializeField]
    int maxAmountOfGoodOrdersForTwoDishes = 20;
    

    [SerializeField]
    List<Dish> dishes = new List<Dish>();
    [SerializeField]
    int timeBonusForCompleteOrder = 3;
    [SerializeField]
    int timePenaltyForOrder = 3;
    [SerializeField]
    int timeForOrderCompletion = 10;
    float currentTimeforOrderCompletion;

    [SerializeField]
    GameObject timerPrefab = null;
    GameObject timerInstance =null;


    private List<GameObject> dishesInstances = new List<GameObject>();
    private List<GameObject> CompletedDishes = new List<GameObject>();
    private List<Vector2> dishesPosition;
    private enum TrayPositions { Left,Right,Middle };
    [SerializeField]
    TrayPositions trayPosition = TrayPositions.Left;
    [Header("Customers")]
    [SerializeField]
    List<GameObject> customers =  new List<GameObject>();
    private GameObject customer = null;
    private bool orderCompleted = false;
    private int timerPositionX = -640;
    //variable for testing 
    //private static int orderDelay = 5;

    bool duringClean = false;


    void Start()
    {
        
        currentTimeforOrderCompletion = timeForOrderCompletion;
        SpawnRandomCustomer();
        setTimerPoisition();
        dishesPosition = setDishesPosition();
        allocateDishesPosition();
        checkForOrderTimeOut();
    }

    // Update is called once per frame
    void Update()
    {
        checkOrderCompletion();
        checkForOrderTimeOut();

    }
    private void SpawnRandomCustomer()
    {
        int randomIndex = UnityEngine.Random.Range(0, customers.Count());
        customer = Instantiate(customers[randomIndex], transform.position, Quaternion.identity);
    }

    private void countTime()
    {
        currentTimeforOrderCompletion -= Time.deltaTime;
        timerInstance.transform.GetChild(0).GetComponent<RadialProgress>().Timer = currentTimeforOrderCompletion;
        if(currentTimeforOrderCompletion <= 7)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    private void checkForOrderTimeOut()
    {
        if (currentTimeforOrderCompletion < 0 && !duringClean)
        {
            timerInstance.transform.GetChild(0).GetComponent<RadialProgress>().Stop();
            FindObjectOfType<GameSession>().substractTime(timePenaltyForOrder);
            StartCoroutine(ClearTrayFull(2));


        }
        else
        {
            countTime();
        }

    }

    private void resetTimer()
    {
        timerInstance.transform.GetChild(0).GetComponent<RadialProgress>().Reset();
        currentTimeforOrderCompletion = timeForOrderCompletion;

    }

    private void setTimerPoisition()
    {
        switch(trayPosition)
        {
            case TrayPositions.Left:
                timerPositionX = -560;
                break;
            case TrayPositions.Middle:
                timerPositionX = 0;
                break;
            case TrayPositions.Right:
                timerPositionX = 560;
                break;
        }
        
        timerInstance = Instantiate(timerPrefab, transform.position, Quaternion.identity);
        Vector2 timerPosition = timerInstance.transform.GetChild(0).transform.position;
        timerInstance.transform.GetChild(0).transform.position = new Vector2(timerPosition.x + timerPositionX, timerPosition.y);
       
        
    }

    private void checkOrderCompletion()
    {
        if (orderCompleted && !duringClean)
        {
            FindObjectOfType<GameSession>().Timer += timeBonusForCompleteOrder;
            StartCoroutine(ClearTray(2));
            
        }
    }
    private List<Vector2> setDishesPosition()
    {
        List<Vector2> tempList = new List<Vector2>();
        for(int i = 0; i< 3; i++)
        {
            tempList.Add(gameObject.transform.GetChild(i).position);
        }
        return tempList;

    }
    private void allocateDishesPosition()
    {
        orderCompleted = false;
        int randomDishIndex = 0;
        int numberOfdishesToAllocate = 0;
        List<Dish> excludingDishesList = new List<Dish>();
        excludingDishesList.AddRange(dishes);
        int goodOrdersForNow = FindObjectOfType<GameSession>().GoodOrders;
        if (goodOrdersForNow < maxAmountOfGoodOrdersForOneDish)
        {
            numberOfdishesToAllocate = UnityEngine.Random.Range(1,2);
        }
        else if(goodOrdersForNow < maxAmountOfGoodOrdersForTwoDishes)
        {
            numberOfdishesToAllocate = UnityEngine.Random.Range(1, 3);
        }
        else
        {
            numberOfdishesToAllocate = 3;
        }
        for (int i = 0;i< numberOfdishesToAllocate; i++)
        {
           randomDishIndex = UnityEngine.Random.Range(0, excludingDishesList.Count());
           dishesInstances.Add(Instantiate(excludingDishesList[randomDishIndex].gameObject, dishesPosition[i],Quaternion.identity));
           excludingDishesList.RemoveAt(randomDishIndex);
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject colidedObject = collision.gameObject;
        if (colidedObject.GetComponent<DragItemScript>().IsDragEnd)
        {
            var name = colidedObject.GetComponent<Dish>().dishName;
            checkForDishMatch(name);
            Destroy(collision.gameObject);
        }
        

    }

    private void checkForDishMatch(Dishes dishName)
    {
        GameObject checkedObject = null;
        checkedObject = dishesInstances.Where(dishObject => dishObject.GetComponent<Dish>().dishName.Equals(dishName)).FirstOrDefault();
        //dish matched
        if(checkedObject != null)
        {
            dishesInstances.Remove(checkedObject);
            CompletedDishes.Add(checkedObject);
            checkedObject.GetComponent<Renderer>().material.SetFloat("_EffectAmount", 0);
            if(dishesInstances.Count == 0)
            {
                orderCompleted = true;
                timerInstance.transform.GetChild(0).GetComponent<RadialProgress>().Stop();
            }
            
        }
        //dish dismatched
        else
        {
            
            FindObjectOfType<GameSession>().substractTime(timePenaltyForOrder);
            StartCoroutine(ClearTrayFull(2));
            timerInstance.SetActive(false);
        }
        
        

    }

    private IEnumerator ClearTrayFull(int time)
    {
        duringClean = true;
        DeactivateTray();
        FindObjectOfType<GameSession>().AddToBadOrders(1);   
        foreach (GameObject objectToDestroy in CompletedDishes)
        {
            Destroy(objectToDestroy);
        }
        foreach (GameObject objectToDestroy in dishesInstances)
        {
            Destroy(objectToDestroy);
        }
        dishesInstances = new List<GameObject>();
        CompletedDishes = new List<GameObject>();
        yield return new WaitForSeconds(time);
        resetTimer();
        allocateDishesPosition();
        AcivateTray();
        duringClean = false;
    }

    

   private IEnumerator ClearTray(int time)
    {
        duringClean = true;
        FindObjectOfType<GameSession>().AddToGoodOrders(1);
        if (currentTimeforOrderCompletion >= timeForOrderCompletion * 0.66)
        {
            FindObjectOfType<GameSession>().AddToScore(Convert.ToInt32(scoreValue * maxTimeScoreMultiplayer));
        }
        else if (currentTimeforOrderCompletion < timeForOrderCompletion * 0.66 && currentTimeforOrderCompletion > timeForOrderCompletion * 0.33)
        {
            FindObjectOfType<GameSession>().AddToScore(Convert.ToInt32(scoreValue * midTimeScoreMultiplayer));
        }
        else 
        {
            FindObjectOfType<GameSession>().AddToScore(Convert.ToInt32(scoreValue * minTimeScoreMultiplayer));
        }

        DeactivateTray();
        
        foreach (GameObject objectToDestroy in CompletedDishes)
        {
            Destroy(objectToDestroy);
        }
        CompletedDishes = new List<GameObject>();
        yield return new WaitForSeconds(time);
        resetTimer();
        allocateDishesPosition();
        AcivateTray();
        duringClean = false;
    }

    private void DeactivateTray()
    {
        customer.SetActive(false);
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
       
    }
    private void AcivateTray()
    {
        timerInstance.SetActive(true);
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        SpawnRandomCustomer();
        customer.SetActive(true);
        

    }

}
