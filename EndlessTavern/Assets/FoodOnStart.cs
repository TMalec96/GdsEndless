using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOnStart : MonoBehaviour
{
    private List<GameObject> dishes = new List<GameObject>();
    void Start()
    {
        float dishSpeed = FindObjectOfType<FoodSpawner>().DishSpeed;
        foreach (Transform child in transform)
        {
            
            child.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-dishSpeed, 0);
        }
    }
}
