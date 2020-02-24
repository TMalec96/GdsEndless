using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dishes
{
    Fish,
    Beer,
    Burger,
    Fries,
    Rum
}
public class Dish : MonoBehaviour
{
    [SerializeField]
    public Dishes dishName;
   
}
