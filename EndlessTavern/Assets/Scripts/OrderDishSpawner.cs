using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderDishSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Vector2> dishesPosition;
    [SerializeField]
    List<Dish> dishes = new List<Dish>();
    List<GameObject> dishesInstances = new List<GameObject>();
    void Start()
    {
        dishesPosition = setDishesPosition();
        allocateDishesPosition();
    }

    // Update is called once per frame
    void Update()
    { 
        if (dishesInstances.Count == 0)
        {
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
        int randomDishIndex = 0;
        for (int i = 0;i< dishesPosition.Count; i++)
        {
            randomDishIndex = Random.Range(0, dishes.Count());
           dishesInstances.Add(Instantiate(dishes[randomDishIndex].gameObject, dishesPosition[i],Quaternion.identity));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var name = collision.gameObject.GetComponent<Dish>().dishName;
        checkForDishMatch(name);
        Destroy(collision.gameObject);

    }

    private void checkForDishMatch(Dishes dishName)
    {
        GameObject destroyedObject = null;
        if (dishesInstances.Count > 0)
        { 
            destroyedObject = dishesInstances.Where(dishObject => dishObject.GetComponent<Dish>().dishName.Equals(dishName)).FirstOrDefault();
            dishesInstances.Remove(destroyedObject);
            Destroy(destroyedObject);
        }

    }
}
