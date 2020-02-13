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

    // Update is called once per frame
    void Start()
    {
        currentValue = timer;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentValue > 0)
        {
            currentValue -= Time.deltaTime;
            ProgressIndicator.text = ((int)currentValue).ToString();

        }

        LoadingBar.fillAmount = currentValue / timer;
    }

    public void Reset()
    {
        currentValue = timer;
        LoadingBar.fillAmount = 0;
    }
}
