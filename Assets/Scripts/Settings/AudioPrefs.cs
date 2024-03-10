using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPrefs : MonoBehaviour
{
    [SerializeField]
    Slider volumeSlider;

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
        //Do whatever we need to do with the saved volume number!
    }
}
