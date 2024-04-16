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
  [Serializable]  public enum SoundNames { ui_button_click, bomb_active, health_collected, enemy_killed, player_hit, emp, health_low}

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

            string formattedSoundName;
            formattedSoundName = s.SoundName.Trim();
            formattedSoundName = s.SoundName.Replace(" ", "_");
        
            
            //formattedSoundName = string.Concat(s.SoundName.Where(c => !char.IsWhiteSpace(c)));
            formattedSoundName = formattedSoundName.ToLower();
            s.SoundName = formattedSoundName;
            if (Enum.IsDefined(typeof(SoundNames), formattedSoundName))
            {
                Debug.Log(String.Format("Enum name match found for: {0}", s.SoundName) );
            }
            else
            {
                throw new Exception("Missing Enum Value. Either Enum list needs updating, or Sound Clips in Audio Manager are incorrectly named");
            }
        }

       
               
                
        

    }

    public  void PlaySound(string nameOfSoundToPlay)
    {
        Sound soundToPlay = Array.Find(soundClips, sound => sound.SoundName == nameOfSoundToPlay);
        soundToPlay.AudioSource.PlayOneShot(soundToPlay.AudioSource.clip);
    }

    public static void PlaySoundStatic(SoundNames nameOfSoundToPlay)
    {
        if(instance == null)
        {
            Debug.LogWarning("Cannot play sound from static method. No Audio Manager Instance detected.");
            return;
        }

        instance.PlaySound(nameOfSoundToPlay.ToString());
    }
}
