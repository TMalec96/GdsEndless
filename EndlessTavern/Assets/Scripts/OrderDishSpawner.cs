using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderDishSpawner : MonoBehaviour
{
    [SerializeField]
    int scoreValue = 100;
    [SerializeField]
    List<Dish> dishes = new List<Dish>();
    [SerializeField]
    int timeBonusForCompleteOrder = 4;
    [SerializeField]
    int timePenaltyForOrder = 4;
    [SerializeField]
    int timeForOrderCompletion = 5;
    float currentTimeforOrderCompletion;

    [SerializeField]
    GameObject timerPrefab;
    GameObject timerInstance;
    

    private List<GameObject> dishesInstances = new List<GameObject>();
    private List<GameObject> CompletedDishes = new List<GameObject>();
    private List<Vector2> dishesPosition;

    private bool orderCompleted = false;
    private static int timersPosition = -640;
    //variable for testing 
    private static int orderDelay = 5;


    void Start()
    {
        //block for testing
        timeForOrderCompletion += orderDelay;
        currentTimeforOrderCompletion = timeForOrderCompletion;
        orderDelay += orderDelay;

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

    private void checkForOrderTimeOut()
    {
        currentTimeforOrderCompletion -= Time.deltaTime;
        if (currentTimeforOrderCompletion < 0)
        {
            clearTrayFull();
            currentTimeforOrderCompletion = timeForOrderCompletion;
            resetTimer();
        }

    }

    private void resetTimer()
    {
        timerInstance.transform.GetChild(0).GetComponent<RadialProgress>().Reset();
    }

    private void setTimerPoisition()
    {
        timerInstance = Instantiate(timerPrefab, transform.position, Quaternion.identity);
        timerInstance.transform.GetChild(0).GetComponent<RadialProgress>().Timer = timeForOrderCompletion;
        Vector2 timerPosition = timerInstance.transform.GetChild(0).transform.position;
        timerInstance.transform.GetChild(0).transform.position = new Vector2(timerPosition.x + timersPosition, timerPosition.y);
        timersPosition += 640;
    }

    private void checkOrderCompletion()
    {
        if (orderCompleted)
        {
            FindObjectOfType<GameSession>().Timer += timeBonusForCompleteOrder;
            clearTray();
            allocateDishesPosition();
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
        for (int i = 0;i< dishesPosition.Count; i++)
        {
           randomDishIndex = UnityEngine.Random.Range(0, dishes.Count());
           dishesInstances.Add(Instantiate(dishes[randomDishIndex].gameObject, dishesPosition[i],Quaternion.identity));
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
            }
            
        }
        //dish dismatched
        else
        {
            FindObjectOfType<GameSession>().Timer -= timePenaltyForOrder;
            clearTrayFull();
        }
        
        

    }

    private void clearTrayFull()
    {
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
        allocateDishesPosition();
    }

    private void clearTray()
    {
        FindObjectOfType<GameSession>().AddToGoodOrders(1);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        foreach (GameObject objectToDestroy in CompletedDishes)
        {
            Destroy(objectToDestroy);
        }
        CompletedDishes = new List<GameObject>();
    }
}
