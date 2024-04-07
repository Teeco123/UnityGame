using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ustawienia : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumeGlobal(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetVolumeFx(float volume)
    {
        audioMixer.SetFloat("Fx", volume);
    }
    public void SetVolumeDialog(float volume)
    {
        audioMixer.SetFloat("Dialogs", volume);
    }
}
