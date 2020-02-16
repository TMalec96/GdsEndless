using System;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
   
    public Text ProgressIndicator;
    public Image LoadingBar;
    float currentValue;
    float timer;

    public float Timer { get => timer; set => timer = value; }

    // Use this for initialization
    private float maxTime;
    private bool stopped = false;
    // Update is called once per frame
    void Start()
    {
        maxTime = timer;
    }

    // Update is called once per frame
    private void Update()
    {
        currentValue = timer;
        
        if (currentValue > 0 && !stopped)
        {
            
            ProgressIndicator.text = ((int)currentValue).ToString();

        }
        else
        {
            ProgressIndicator.text = "";
        }

        LoadingBar.fillAmount = currentValue / maxTime;
    }

    public void Reset()
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.enabled = true;
        stopped = false;
    }

    public void Stop()
    {
        stopped = true;
        LoadingBar.fillAmount = 0;
        ProgressIndicator.text = "";
        LoadingBar.enabled = false;
    }
}
