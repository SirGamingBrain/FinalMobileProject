using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class GameAudioScript : MonoBehaviour
{
    public Sound[] sounds;

    public AudioSource bgMusic;

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
        float i = UnityEngine.Random.Range(0, 1);

        if (i == 1)
        {
            Play("Track 1", bgMusic);
        }
        else
        {
            Play("Track 2", bgMusic);
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

    public void Play(string name, AudioSource source)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("No sound found of name: " + name);
            return;
        }

        Debug.Log("Playing sound clip named: " + name);

        s.source = source;
        s.source.clip = s.clip;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.volume = s.volume * (PlayerPrefs.GetFloat("Master Volume") * .01f);
        s.source.Play();
    }
}
