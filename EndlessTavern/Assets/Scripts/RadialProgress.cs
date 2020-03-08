using System;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgress : MonoBehaviour
{
   

    public Image LoadingBar;
    float currentValue;
    float timer;

    public float Timer { get => timer; set => timer = value; }

    // Use this for initialization
    private float maxTime;
    private double oneThirdMaxTime;
    private double twoThirdMaxTime;
    // Update is called once per frame
    void Start()
    {
        LoadingBar.color = new Color(0, 0.5F, 0,1);
        maxTime = timer;
        oneThirdMaxTime = maxTime * 0.33;
        twoThirdMaxTime = maxTime * 0.66;
    }

    // Update is called once per frame
    private void Update()
    {
        currentValue = timer;
        if (currentValue <= oneThirdMaxTime)
        {
            LoadingBar.color = Color.red;
        }
        else if (currentValue <= twoThirdMaxTime)
        {
            LoadingBar.color = Color.yellow;
        }
       
            LoadingBar.fillAmount = currentValue / maxTime;
    }

    public void Reset()
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.enabled = true;
    }

    public void Stop()
    {
        LoadingBar.fillAmount = 0;
        LoadingBar.enabled = false;
    }
}
