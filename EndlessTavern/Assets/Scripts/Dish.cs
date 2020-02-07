using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dishes
{
    Chicken_leg,
    Pie,
    Burger,
    Fries,
    Pizza
}
public class Dish : MonoBehaviour
{
    [SerializeField]
    public Dishes dishName;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
