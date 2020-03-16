using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostDishesTable : MonoBehaviour
{
    private Transform entryContainter;
    private Transform entryTemplate;

    private List<Transform> mostDishesEntryTransformList;
    private List<MostDishesEntry> mostDishesEntryList;
    private int biggestDishesNumber;
    private List<Transform> recordsInstances = new List<Transform>();

    public int BiggestDishesNumber { get => biggestDishesNumber; set => biggestDishesNumber = value; }

    private void Awake()
    {
        entryContainter = transform.Find("EntryContainer");
        entryTemplate = entryContainter.Find("EntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        LoadMostDishesTable();

    }

    private void LoadMostDishesTable()
    {
        string jsonString = PlayerPrefs.GetString("mostDishesTable");
        MostDishes mostDishes = JsonUtility.FromJson<MostDishes>(jsonString);

        for (int i = 0; i < mostDishes.mostDishesEntryList.Count; i++)
        {
            for (int j = i + 1; j < mostDishes.mostDishesEntryList.Count; j++)
            {
                if (mostDishes.mostDishesEntryList[j].score > mostDishes.mostDishesEntryList[i].score)
                {
                    MostDishesEntry tmp = mostDishes.mostDishesEntryList[i];
                    mostDishes.mostDishesEntryList[i] = mostDishes.mostDishesEntryList[j];
                    mostDishes.mostDishesEntryList[j] = tmp;
                }
            }
        }
        biggestDishesNumber = mostDishes.mostDishesEntryList[0].score;

        mostDishesEntryTransformList = new List<Transform>();
        foreach (MostDishesEntry mostDishesEntry in mostDishes.mostDishesEntryList)
        {
            CreateMostDishesEntryTransform(mostDishesEntry, entryContainter, mostDishesEntryTransformList);
        }
    }

    private void CreateMostDishesEntryTransform(MostDishesEntry mostDishesEntry, Transform container, List<Transform> transformList)
    {
        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        recordsInstances.Add(entryTransform);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("score").GetComponent<Text>().text = mostDishesEntry.name;
        entryTransform.Find("player").GetComponent<Text>().text = mostDishesEntry.score.ToString();
        transformList.Add(entryTransform);

    }

    public void AddMostDishesEntry(int score, string name)
    {
        foreach (Transform recordsInstance in recordsInstances)
        {
            Destroy(recordsInstance.gameObject);
        }
        //Create HighscoreEntry
        MostDishesEntry mostDishesEntry = new MostDishesEntry { score = score, name = name };
        //Load saved mostdishes
        string jsonString = PlayerPrefs.GetString("mostDishesTable");
        MostDishes mostDishes = new MostDishes();
        if (jsonString != "")
        {
            mostDishes = JsonUtility.FromJson<MostDishes>(jsonString);
        }
        //Add new entry to Highscores
        mostDishes.mostDishesEntryList.Add(mostDishesEntry);
        //Save updated Highscores
        string json = JsonUtility.ToJson(mostDishes);
        PlayerPrefs.SetString("mostDishesTable", json);
        PlayerPrefs.Save();
        LoadMostDishesTable();

    }
    public void ResetMostDishesTable()
    {
        foreach(Transform recordsInstance in recordsInstances)
        {          
            Destroy(recordsInstance.gameObject);
        }
        MostDishesEntry mostDishesEntry = new MostDishesEntry { score = 0, name = "" };
        MostDishes mostDishes = new MostDishes();
        mostDishes.mostDishesEntryList.Add(mostDishesEntry);
        //Save updated MostDishes
        string json = JsonUtility.ToJson(mostDishes);
        PlayerPrefs.SetString("mostDishesTable", json);
        PlayerPrefs.Save();
        LoadMostDishesTable();
    }

    private class MostDishes
    {
        public List<MostDishesEntry> mostDishesEntryList = new List<MostDishesEntry>();
    }
    [System.Serializable]
    private struct MostDishesEntry
    {
        public int score;
        public string name;
    }
}
