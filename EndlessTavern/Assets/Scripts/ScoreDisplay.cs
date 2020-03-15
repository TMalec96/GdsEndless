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
        scoreText = transform.Find("Score").gameObject.GetComponent<Text>();
        goodOrdersText = transform.Find("GoodOrdersScore").gameObject.GetComponent<Text>();
        badOrdersText = transform.Find("BadOrdersScore").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = gameSession.Score.ToString();
        goodOrdersText.text = gameSession.BadOrders.ToString();
        badOrdersText.text = gameSession.GoodOrders.ToString();
             
    }
}
