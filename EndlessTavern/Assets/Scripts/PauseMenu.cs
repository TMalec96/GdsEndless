using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    Canvas maincCanvas;
    private bool paused = false;
    public bool Paused { get => paused; set => paused = value; }
    public void Pause()
    {


        if (Time.timeScale == 0f)
        {
           
            Time.timeScale = 1f;
            paused = false;
        }
        else
        {
            
            Time.timeScale = 0f;
            paused = true;

        }
    }
    void OnGUI()
    {
        if (paused && maincCanvas != null)
        {
            maincCanvas.transform.Find("PauseMenuImage").gameObject.SetActive(true);

        }
        else if (!paused && maincCanvas != null)
        {
            maincCanvas.transform.Find("PauseMenuImage").gameObject.SetActive(false);
            maincCanvas.transform.Find("SettingsMenuImage").gameObject.SetActive(false);
        }
    }
}
