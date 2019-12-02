using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class AudioScript : MonoBehaviour
{
    public Sound[] sounds;

    public void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source.clip = s.clip;

            s.source.volume = s.volume * (PlayerPrefs.GetFloat("Master Volume") * .01f);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Play("Main Menu Music");
        }
    }

    public void VolumeChange()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
                s.source.volume = s.volume * (PlayerPrefs.GetFloat("Master Volume") * .01f);
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("No sound found of name: " + name);
            return;
        }
        Debug.Log("Playing sound clip named: " + name);
        s.source.Play();
    }
}
