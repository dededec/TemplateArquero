using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public void ToggleAudioSource(AudioSource audioSource)
    {
        if(audioSource.volume != 0) audioSource.volume = 0;
        else audioSource.volume = 1;
    }

    public void GoToLink(string link)
    {
        Application.OpenURL(link);
    }

}
