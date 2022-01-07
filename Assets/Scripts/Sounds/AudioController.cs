using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public PlayerController volume;
    public float specificVolume = 1f;
    private AudioSource sound;
    public string soundType;

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        if (soundType == "sfx")
        {
            sound.volume = volume.getMasterVolume() * volume.getSfxVolume() * specificVolume;
        }
        else if (soundType == "music")
        {
            sound.volume = volume.getMasterVolume() * volume.getMusicVolume() * specificVolume;
        }
        else if (soundType == "voice")
        {
            sound.volume = volume.getMasterVolume() * volume.getVoiceVolume() * specificVolume;
        }
    }

    public void playSound()
    {
        if (soundType == "sfx")
        {
            sound.volume = volume.getMasterVolume() * volume.getSfxVolume() * specificVolume;
        }
        else if (soundType == "music")
        {
            sound.volume = volume.getMasterVolume() * volume.getMusicVolume() * specificVolume;
        }
        else if (soundType == "voice")
        {
            sound.volume = volume.getMasterVolume() * volume.getVoiceVolume() * specificVolume;
        }
        sound.Play();
    }

    public void setSpecificVolume(float newVolume)
    {
        specificVolume = newVolume;
    }
}
