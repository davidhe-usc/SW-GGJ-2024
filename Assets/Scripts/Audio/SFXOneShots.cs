using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOneShots : MonoBehaviour
{
    public static SFXOneShots instance;

    private AudioPlayOneShot audioPlayer;
    private void Awake()
    {
        SFXOneShots[] tms = FindObjectsOfType<SFXOneShots>();

        if (tms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;

        audioPlayer = GetComponent<AudioPlayOneShot>();
    }

    public void PlayOneShot(AudioClipCueSO audioCue)
    {
        audioPlayer.Play(audioCue);
    }

    private void PlayInternal(AudioClipCueSO audioCue)
    {

    }
}
