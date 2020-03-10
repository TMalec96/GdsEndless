using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiScoreTable : MonoBehaviour
{
    private Transform entryContainter;
    private Transform entryTemplate;

    private List<Transform> hiscoreEntryTransformList;
    private List<HiscoreEntry> hiscoreEntryList;
    private int biggestScore;

    public int BiggestScore { get => biggestScore; set => biggestScore = value; }

    private void Awake()
    {
        entryContainter = transform.Find("EntryContainer");
        entryTemplate = entryContainter.Find("EntryTemplate");
        
        loadHiscoresTable();
       
    }

    private void loadHiscoresTable()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.hiscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.hiscoreEntryList.Count; j++)
            {
                if (highscores.hiscoreEntryList[j].score > highscores.hiscoreEntryList[i].score)
                {
                    HiscoreEntry tmp = highscores.hiscoreEntryList[i];
                    highscores.hiscoreEntryList[i] = highscores.hiscoreEntryList[j];
                    highscores.hiscoreEntryList[j] = tmp;
                }
            }
        }
        biggestScore = highscores.hiscoreEntryList[0].score;

        hiscoreEntryTransformList = new List<Transform>();
        foreach (HiscoreEntry hiscoreEntry in highscores.hiscoreEntryList)
        {
            CreateHiscoreEntryTransform(hiscoreEntry, entryContainter, hiscoreEntryTransformList);
        }
    }

    private void CreateHiscoreEntryTransform(HiscoreEntry hiscoreEntry, Transform container, List<Transform> transformList)
    {
        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("score").GetComponent<Text>().text = hiscoreEntry.name;
        entryTransform.Find("player").GetComponent<Text>().text = hiscoreEntry.score.ToString();
        transformList.Add(entryTransform);

    }

    public void AddHiscoreEntry(int score,string name)
    {
        //Create HighscoreEntry
        HiscoreEntry highscoreEntry = new HiscoreEntry { score = score, name = name };
        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = new Highscores();
        if (jsonString != "")
        {
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        //Add new entry to Highscores
        highscores.hiscoreEntryList.Add(highscoreEntry);
        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        loadHiscoresTable();

    }
    public void ResetHiscoresTable()
    {
        HiscoreEntry highscoreEntry = new HiscoreEntry { score = 0, name = "Player" };
        Highscores highscores = new Highscores();
        highscores.hiscoreEntryList.Add(highscoreEntry);
        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        loadHiscoresTable();
    }
    
    private class Highscores
    {
        public List<HiscoreEntry> hiscoreEntryList = new List<HiscoreEntry>();
    }
    [System.Serializable]
    private struct HiscoreEntry
    {
        public int score;
        public string name;
    }
}
