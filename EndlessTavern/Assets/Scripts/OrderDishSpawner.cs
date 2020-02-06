using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDishSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Vector2> dishesPosition;
    [SerializeField]
    List<Dish> dishes;
    void Start()
    {
        dishesPosition = setDishesPosition();
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private List<Vector2> setDishesPosition()
    {
        List<Vector2> tempList = new List<Vector2>();
        for(int i = 0; i< 3; i++)
        {
            tempList.Add(gameObject.transform.GetChild(i).position);
            print(gameObject.transform.GetChild(i).position);
        }
        return tempList;

    }
    private void allocateDishesPosition()
    {
        for (int i = 0;i< dishesPosition.Capacity; i++)
        {

        }
    }
}
