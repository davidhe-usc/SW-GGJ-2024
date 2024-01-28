using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDrumRoll : MonoBehaviour
{
    private AudioPlayCue audioPlayer;
    public static MusicDrumRoll instance;

    private void Awake()
    {
        MusicDrumRoll[] tms = FindObjectsOfType<MusicDrumRoll>();

        if (tms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        audioPlayer = GetComponent<AudioPlayCue>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDrumRoll()
    {
        audioPlayer.Play(true);
    }

    public void StopDrumRoll()
    {
        audioPlayer.Stop(true);
        SFXOneShots.instance.PlayOneShot(SFXOneShots.instance.sfxDialogueSelect);
    }
}
