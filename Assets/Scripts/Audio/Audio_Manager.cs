using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Reflection.Emit;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] private  Sound[] soundClips;
    public enum SoundNames { }

    private static Audio_Manager instance;

    private void Awake()
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

        CreateSoundSourceComponented();
    }

    private void CreateSoundSourceComponented()
    {
        foreach(Sound s in soundClips)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.Clip;
            s.AudioSource.pitch = s.Pitch;
            s.AudioSource.volume = s.Volume;
            
        }
    }

    public  void PlaySound(string nameOfSoundToPlay)
    {
        Sound soundToPlay = Array.Find(soundClips, sound => sound.SoundName == nameOfSoundToPlay);
        soundToPlay.AudioSource.PlayOneShot(soundToPlay.AudioSource.clip);
    }

    public static void PlaySoundStatic(string nameOfSoundToPlay)
    {
        if(instance == null)
        {
            Debug.LogWarning("Cannot play sound from static method. No Audio Manager Instance detected.");
            return;
        }

        instance.PlaySound(nameOfSoundToPlay);
    }
}
