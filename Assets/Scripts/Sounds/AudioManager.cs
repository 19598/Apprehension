using System;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    [Header("To Use This Audio Manager In Script Use The Comment Provided Inside")]
    //Simply copy and paste the following line into any piece of code and reference the name of the sound and it will play wherever it is put.
    //FindObjectOfType<AudioManager>().Play("Name of sound");
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
