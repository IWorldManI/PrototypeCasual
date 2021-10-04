using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public GameObject soundB;
    public GameObject vibroB;

    private void Start()
    {
        soundB.GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        vibroB.GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt("VibroEnabled", 1) == 1;
    }

    public void SetAudio(bool isMusic)
    {
        if (isMusic)
            Mixer.audioMixer.SetFloat("volume", 0);
        else
            Mixer.audioMixer.SetFloat("volume", -80);

        PlayerPrefs.SetInt("MusicEnabled", isMusic ? 1 : 0);
        Debug.Log(PlayerPrefs.GetInt("MusicEnabled"));
        PlayerPrefs.Save();
    }
    public void SetVibro(bool isVibro)
    {
        PlayerPrefs.SetInt("VibroEnabled", isVibro ? 1 : 0);
        PlayerPrefs.Save();
    }
}
