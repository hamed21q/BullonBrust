using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public List<Audio> audioClips;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Audio audio in audioClips)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
            audio.source.playOnAwake = audio.playOnAwake;
        }
    }
    public void Play(string name)
    {
        find(name)?.source.Play();
    }
    public void Play(Audio audio)
    {
        audio.source.Play();
    }
    
    public Audio find(string name)
    {
        foreach (Audio audio in audioClips)
        {
            if (audio.name == name)
            {
                return audio;
            }
        }
        return null;
    }
    
}
[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    [Range(0, 1)]
    public float volume = 1;
    [Range(0.3f, 3)]
    public float pitch = 1.5f;
    public bool loop = false;
    public bool playOnAwake = false;
}
