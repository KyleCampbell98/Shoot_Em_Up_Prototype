using System;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class Sound
{
    [SerializeField] private string soundName;
    [SerializeField] private AudioClip soundClip;
    [Range(0, 1)][SerializeField] private float volume;
    [Range(.1f, 3)][SerializeField] private float pitch;

   private AudioSource audioSource;

    public AudioSource AudioSource { get
        {
            return audioSource;
        }
        set { audioSource = value; } }

    public string SoundName {  get { return soundName; } set { soundName = value; } }
    public AudioClip Clip { get { return soundClip; } set { soundClip = value; } }
    public float Volume { get { return volume; } set { volume = value; } }
    public float Pitch { get { return pitch; } set { pitch = value; } }
}
