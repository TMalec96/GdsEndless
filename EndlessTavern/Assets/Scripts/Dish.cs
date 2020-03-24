using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dishes
{
    Fish,
    Shake,
    Burger,
    Fries,
    Pizza
}
public class Dish : MonoBehaviour
{
    [SerializeField]
    public Dishes dishName;
   
}
