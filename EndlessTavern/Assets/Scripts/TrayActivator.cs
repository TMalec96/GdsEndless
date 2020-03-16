using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayActivator : MonoBehaviour
{

    private List<OrderDishSpawner> trays = new List<OrderDishSpawner>();
    [SerializeField]
    Canvas gameCanvas;
    [SerializeField]
    int timeToPrepear = 6;
    GameObject getReadyText;
    void Start()
    {
        getReadyText = gameCanvas.transform.Find("GetReadyText").gameObject;
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
            if (i == 0)
            {
                yield return new WaitForSeconds(timeToPrepear);
                trays[i].gameObject.SetActive(true);
            }
            else
            {
                getReadyText.SetActive(false);
                yield return new WaitForSeconds(1);
                trays[i].gameObject.SetActive(true);
            }
           
            
        }
    }
   
}
