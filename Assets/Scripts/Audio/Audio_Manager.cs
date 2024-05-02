using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection.Emit;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] private Sound[] soundClips;
    [Serializable] public enum SoundNames { speed_up, ui_button_click, bomb_active, health_collected, enemy_killed, player_hit, emp, health_low, bomb_placed, background_music }

    private static Audio_Manager instance;

    // Monobehaviour scripts

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
    private void Start()
    {
        PlaySound(SoundNames.background_music, false);
    }

    private void CreateSoundSourceComponented()
    {
        foreach (Sound s in soundClips)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.Clip;
            s.AudioSource.pitch = s.Pitch;
            s.AudioSource.volume = s.Volume;
            s.AudioSource.loop = s.ShouldLoopClip;

            string formattedSoundName = FormatAudioName(s.SoundName);

            //s.SoundName = formattedSoundName;
            if (Enum.IsDefined(typeof(SoundNames), formattedSoundName))
            {
               // Debug.Log(String.Format("Enum name match found for: {0}. Enum Equivelant is: {1}.", s.SoundName, formattedSoundName));
            }
            else
            {
                throw new Exception("Missing Enum Value. Either Enum list needs updating, or Sound Clips in Audio Manager are incorrectly named");
            }
        }






    }

    private void PlaySound(SoundNames nameOfSoundToPlay, bool isShortClip)
    {
        Sound soundToPlay = Array.Find(soundClips, sound => FormatAudioName(sound.SoundName) == nameOfSoundToPlay.ToString());
        if (isShortClip) { 
        
        soundToPlay.AudioSource.PlayOneShot(soundToPlay.AudioSource.clip);
    }
        else
        {
            soundToPlay.AudioSource.Play();
            
        }
}
    


    public static void PlaySoundStatic(SoundNames nameOfSoundToPlay)
    {

        if(instance == null)
        {
            Debug.LogWarning("Cannot play sound from static method. No Audio Manager Instance detected.");
            return;
        }

        instance.PlaySound(nameOfSoundToPlay, true);
    }


    // Audio Naming Utility
    private string FormatAudioName(string stringToFormat)
    {
        string formattedAudioName;
        formattedAudioName = stringToFormat.Trim();
        formattedAudioName = formattedAudioName.Replace(" ", "_");

        formattedAudioName = formattedAudioName.ToLower();
        return formattedAudioName;
    }
}
