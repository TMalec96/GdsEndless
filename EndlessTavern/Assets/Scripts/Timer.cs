using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   
    private Text timerText;
    

    // Start is called before the first frame update
    void Start()
    {
   
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = ((int)FindObjectOfType<GameSession>().Timer).ToString();
    }
}
