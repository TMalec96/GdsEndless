using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioMixer masterMixer;
    private bool isMusicOn = true;
    public bool IsMusicOn
    {
        get { return isMusicOn; }
        set { isMusicOn = value; }
    }
    private bool isSfxOn = true;
    public bool IsSfxOn
    {
        get { return isSfxOn; }
        set { isSfxOn = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        int numberAudioControllers = FindObjectsOfType<AudioController>().Length;
        if (numberAudioControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
   public void TurnOnMusic()
    {
            masterMixer.SetFloat("MusicVolume", 0);
            isMusicOn = true;
       
 
    }
    public void TurnOnSFX()
    {
            masterMixer.SetFloat("SfxVolume", 0);
            isSfxOn = true;
      
    }
    public void TurnOffSFX()
    {
        masterMixer.SetFloat("SfxVolume", -80);
        isSfxOn = false;

    }
    public void TurnOffMusic()
    {
        masterMixer.SetFloat("MusicVolume",-80);
        isMusicOn = false;

    }


}
