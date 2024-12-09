using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Image ObjectMusic;
    private float _soundAndMusicVolume = 0f;
    [SerializeField] AudioMixerGroup _AudioMixerGroup;
    float _out;

    void Start()
    {
        
    }

    public void SwitchSoundAndMusicVolume()
    {
        if (_AudioMixerGroup.audioMixer.GetFloat("General", out _out))
        {
            if (_out == -5)
            {
                _AudioMixerGroup.audioMixer.SetFloat("General", -15);
            }
            if (_out == -15)
            {
                _AudioMixerGroup.audioMixer.SetFloat("General", -80);
            }
            if (_out == -15)
            {
                _AudioMixerGroup.audioMixer.SetFloat("General", -5);
            }
        }
    }
}
