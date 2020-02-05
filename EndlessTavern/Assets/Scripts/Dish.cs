using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public enum DishColors
    {
        White,
        Red,
        Yellow,
        Blue,
        Pink
    }
    [SerializeField]
    DishColors dishColor = DishColors.Blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
