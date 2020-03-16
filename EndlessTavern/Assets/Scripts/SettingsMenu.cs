using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void ActivateSettingsMenu()
    {
        gameObject.SetActive(true);
    }
    public void DeactivateSettingsMenu()
    {
        gameObject.SetActive(false);
    }
}
