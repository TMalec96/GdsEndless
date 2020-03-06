using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayActivator : MonoBehaviour
{

    private List<OrderDishSpawner> trays = new List<OrderDishSpawner>();
    void Start()
    {
        GetChilds();
        trays[0].gameObject.SetActive(true);

    }
    private void GetChilds()
    {
        foreach (Transform child in transform)
        {
            trays.Add(child.GetComponent<OrderDishSpawner>());
        }
    }
    public void SetActiveTraysNumber(int number)
    {
        for (int i =0; i < number; i++)
        {
            trays[i].gameObject.SetActive(true);
        }
    }
   
}
