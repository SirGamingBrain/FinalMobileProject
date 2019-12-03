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
    
    public void FadeOut()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
                StartCoroutine(FadingOut(s.source, 1f, 0f));
            }
        }
    }

    public static IEnumerator FadingOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void FadeIn()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
                StartCoroutine(FadingIn(s.source, 1f, 1f));
            }
        }
    }

    public static IEnumerator FadingIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
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
