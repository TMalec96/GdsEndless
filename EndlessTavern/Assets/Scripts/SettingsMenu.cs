using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Toggle musicToggle;
    Toggle sfxToggle;
    private void Start()
    {
        InitializeToggles();
    }
    public void ActivateSettingsMenu()
    {
        gameObject.SetActive(true);
    }
    public void DeactivateSettingsMenu()
    {
        gameObject.SetActive(false);
    }
    private void InitializeToggles()
    {
        
        musicToggle = transform.Find("MusicToggle").gameObject.GetComponent<Toggle>();
        sfxToggle = transform.Find("SFXToggle").gameObject.GetComponent<Toggle>();
        musicToggle.isOn = UpdateMusicToggleStatus();
        sfxToggle.isOn = UpdateSFXMusicToggleStatus();
        
    }
    public bool UpdateMusicToggleStatus()
    {
       return FindObjectOfType<AudioController>().IsMusicOn;
    }
    public bool UpdateSFXMusicToggleStatus()
    {
        return FindObjectOfType<AudioController>().IsSfxOn;

    }
    public void SwitchMusic()
    {
        
            if (UpdateMusicToggleStatus() && UpdateMusicToggleStatus() != musicToggle.isOn)
            {
                FindObjectOfType<AudioController>().TurnOffMusic();
            }
            else if(!UpdateMusicToggleStatus() && UpdateMusicToggleStatus() != musicToggle.isOn)
            {
                FindObjectOfType<AudioController>().TurnOnMusic();
            }
        
    }
    public void SwitchSFX()
    {
        if (UpdateSFXMusicToggleStatus() && UpdateSFXMusicToggleStatus() != sfxToggle.isOn)
        {
            FindObjectOfType<AudioController>().TurnOffSFX();
        }
        else if (!UpdateSFXMusicToggleStatus() && UpdateSFXMusicToggleStatus() != sfxToggle.isOn)
        {
            FindObjectOfType<AudioController>().TurnOnSFX();
        }
    }
    
}
