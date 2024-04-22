using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class Button_Base_Logic : MonoBehaviour
{
    public Audio_Manager.SoundNames soundNames;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayOnClickAudio);
    }

    private void PlayOnClickAudio()
    {
       Audio_Manager.PlaySoundStatic(soundNames);
    }
}
