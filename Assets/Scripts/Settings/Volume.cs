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
        Master.value = PlayerPrefs.GetFloat("Master");
        audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));

        Music.value = PlayerPrefs.GetFloat("Music");
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));

        Fx.value = PlayerPrefs.GetFloat("Fx");
        audioMixer.SetFloat("Fx", PlayerPrefs.GetFloat("Fx"));

        Dialogs.value = PlayerPrefs.GetFloat("Dialogs");
        audioMixer.SetFloat("Dialogs", PlayerPrefs.GetFloat("Dialogs"));

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
