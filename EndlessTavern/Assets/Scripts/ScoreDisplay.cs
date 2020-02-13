using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    
    Text scoreText;
    Text goodOrdersText;
    Text badOrdersText;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        mapChildsTexts();
    }

    private void mapChildsTexts()
    {
        scoreText = transform.GetChild(0).gameObject.GetComponent<Text>();
        goodOrdersText = transform.GetChild(2).gameObject.GetComponent<Text>();
        badOrdersText = transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = gameSession.Score.ToString();
        goodOrdersText.text = gameSession.BadOrders.ToString();
        badOrdersText.text = gameSession.GoodOrders.ToString();
             
    }
}
