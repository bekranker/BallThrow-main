using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Slider VolumeSlider;
    public GameObject ObjectMusic;

    public GameObject MusicOnIcon;
    public GameObject MusicOffIcon;

    private float MusicVolume = 0f;
    private AudioSource Audiosource;
    //AudioSource a DoNotDestroy kodunu atarak kullan.
    
    void Start()
    {
       
        ObjectMusic = GameObject.FindWithTag("GameMusic");
        Audiosource = ObjectMusic.GetComponent<AudioSource>();

        MusicVolume = PlayerPrefs.GetFloat("volume" ,1);
        Audiosource.volume = MusicVolume;
        VolumeSlider.value = MusicVolume;
    }

    
    void Update()
    {
        MusicVolume = VolumeSlider.value;
        Audiosource.volume = MusicVolume;
        PlayerPrefs.SetFloat("volume", MusicVolume);

        if(VolumeSlider.value == 0)
        {
            MusicOffIcon.SetActive(true);
            MusicOnIcon.SetActive(false);
        }
        else
        {
            MusicOffIcon.SetActive(false);
            MusicOnIcon.SetActive(true);
        }
    }

    public void VolumeUpdater(float volume)
    {
        MusicVolume = volume;
    }

    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("volume");
        Audiosource.volume = 1;
        VolumeSlider.value = 1;
    }

    public void MusicOff()
    {
        if(VolumeSlider.value == 0)
        {
            if(PlayerPrefs.GetFloat("volume") == 0)
            {
                VolumeSlider.value = 0.1f;
            }
            else
            {
                VolumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
                
            }
        }
        else
        {
            VolumeSlider.value = 0;
        }
    }
}
