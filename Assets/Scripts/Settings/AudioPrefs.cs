using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioPrefs : MonoBehaviour
{
    [SerializeField]
    Slider volumeSlider;
    [SerializeField]
    AudioMixer mixer;

    private void Start()
    {
        CheckFirstLoad();
        LoadVolumeFromSave();
    }


    public void LoadVolumeFromSave()
    {
        volumeSlider.value = GetVolume();
        UpdateGameVolume();
        
    }

    void CheckFirstLoad()
    {
        if (PlayerPrefs.GetString("FirstTimeLoadingVolume") != "Loaded")
        {
            ResetControls();
        }
    }

    
    public void ResetControls()
    {
        PlayerPrefs.SetString("FirstTimeLoadingVolume", "Loaded");

        PlayerPrefs.SetFloat("MasterVolume", 1);

        LoadVolumeFromSave();
    }

    public void SaveVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
    }

    public void SaveVolumeFromSlider()
    {
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("MasterVolume");
    }

    public void UpdateGameVolume()
    {
        float volClamped = Mathf.Clamp(PlayerPrefs.GetFloat("MasterVolume"), 0.0001f, 1f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(volClamped) * 20);
    }
}
