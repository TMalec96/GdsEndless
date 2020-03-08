using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayActivator : MonoBehaviour
{

    private List<OrderDishSpawner> trays = new List<OrderDishSpawner>();
    void Start()
    {
        GetChilds();
        StartCoroutine(SetActiveTraysNumber(3));

    }
    private void GetChilds()
    {
        foreach (Transform child in transform)
        {
            trays.Add(child.GetComponent<OrderDishSpawner>());
        }
    }
    public IEnumerator SetActiveTraysNumber(int number)
    {
        for (int i =0; i < number; i++)
        {
            yield return new WaitForSeconds(2);
            trays[i].gameObject.SetActive(true);
            
        }
    }
   
}
