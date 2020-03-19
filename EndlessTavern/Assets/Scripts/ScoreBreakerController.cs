using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBreakerController : MonoBehaviour
{
    private enum recordType { Orders,Score,Both,None};
    private recordType recordBrokeType = recordType.None;
    private int currentScore;
    private int currentGoodOrders;
    Transform recordBrakerText;
    Transform inputField;
    [SerializeField]
    string congratsTextBiggestScore = "You shown them!";
    [SerializeField]
    string congratsTextBiggestOrders = "You serve those plates fast!";
    [SerializeField]
    string congratsTextBiggesBoth = "You are the best!";
    [SerializeField]
    string congratsTextNoRecordBreak = "Try better next time!";
    // Start is called before the first frame update
    void Awake()
    {
        currentGoodOrders = FindObjectOfType<GameSession>().GoodOrders;
        currentScore = FindObjectOfType<GameSession>().Score;
        recordBrakerText = transform.Find("RecordBrakerText");
        inputField = transform.Find("InputField");
        recordBrakerText.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        checkForHiScoreBreak();

    }

    private void checkForHiScoreBreak()
    {
        int maxScore = FindObjectOfType<HiScoreTable>().BiggestScore;
        int maxOrders = FindObjectOfType<MostDishesTable>().BiggestDishesNumber;

        if(currentScore> maxScore)
        {
            
            recordBrakerText.gameObject.GetComponent<Text>().text = congratsTextBiggestScore;
            recordBrakerText.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            recordBrokeType = recordType.Both;
        }
        else if(currentGoodOrders> maxOrders)
        {
           
            recordBrakerText.gameObject.GetComponent<Text>().text = congratsTextBiggestOrders;
            recordBrakerText.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            recordBrokeType = recordType.Both;
        }
        else if(currentScore > maxScore && currentGoodOrders > maxOrders)
        {
          
            recordBrakerText.gameObject.GetComponent<Text>().text = congratsTextBiggesBoth;
            recordBrakerText.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            recordBrokeType = recordType.Both;
        }
        else if (currentScore <= maxScore && currentGoodOrders <= maxOrders)
        {
            
            recordBrakerText.gameObject.GetComponent<Text>().text = congratsTextNoRecordBreak;
            recordBrakerText.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            recordBrokeType = recordType.Both;
        }
    }

    public void SetRecord()
    {
        string name = inputField.GetComponent<InputField>().text;
        switch (recordBrokeType)
        {
            case recordType.Score:
                FindObjectOfType<HiScoreTable>().AddHiscoreEntry(currentScore, name);
                break;
            case recordType.Orders:
                FindObjectOfType<MostDishesTable>().AddMostDishesEntry(currentGoodOrders, name);
                break;
            case recordType.Both:
                FindObjectOfType<MostDishesTable>().AddMostDishesEntry(currentGoodOrders, name);
                FindObjectOfType<HiScoreTable>().AddHiscoreEntry(currentScore, name);
                break;
        }
        inputField.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
