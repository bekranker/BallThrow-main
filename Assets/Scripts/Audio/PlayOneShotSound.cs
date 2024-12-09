using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayOneShotSound : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip Sound;

    public void PlayOneSHOT()
    {
        sound.PlayOneShot(Sound, sound.volume);
    }
}