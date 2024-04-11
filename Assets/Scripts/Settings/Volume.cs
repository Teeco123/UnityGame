using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Ustawienia : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider Master, Music, Fx, Dialogs;

    private void Start()
    {
        string[] nazwakanau = { "Master", "Music", "Fx", "Dialogs" };

        foreach (string s in nazwakanau)
        {
            float storedValue = PlayerPrefs.GetFloat(s);
            audioMixer.SetFloat(s, storedValue);

            switch (s)
            {
                case "Master":
                    Master.value = storedValue;
                    break;
                case "Music":
                    Music.value = storedValue;
                    break;
                case "Fx":
                    Fx.value = storedValue;
                    break;
                case "Dialogs":
                    Dialogs.value = storedValue;
                    break;
            }
        }
    }

    public void SetVolumeGlobal(float volume)
    {
        
        audioMixer.SetFloat("Master", volume);
        savepref("Master", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        savepref("Music", volume);
    }
    public void SetVolumeFx(float volume)
    {
        audioMixer.SetFloat("Fx", volume);
        savepref("Fx", volume);
    }
    public void SetVolumeDialog(float volume)
    {
        audioMixer.SetFloat("Dialogs", volume);
        savepref("Dialogs", volume);
    }

    void savepref(string name,float volume)
    {
        PlayerPrefs.SetFloat(name, volume);
    }
}
