using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private AudioPlayCue ambA, ambB, ambC, ambD;

    private int tempoThresholdB = 10;
    private int tempoThresholdC = 25;
    private int tempoThresholdD = 40;

    public static AmbienceManager instance;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;  

        DontDestroyOnLoad(gameObject);
    }

    public void CheckAmbienceIntensity (int tempo)
    {
        if (tempo >= tempoThresholdD)
            ambD.Play(true);
        else
            ambD.Stop(true);

        if (tempo >= tempoThresholdC)
            ambC.Play(true);
        else
            ambC.Stop(true);

        if (tempo >= tempoThresholdB)
            ambB.Play(true);
        else
            ambB.Stop(true);
    }

    public void StopAmbience()
    {
        ambA.Stop(false);
        ambB.Stop(false);
        ambC.Stop(false);
        ambD.Stop(false);
    }

    public void StartAmbience()
    {
        ambA.Play(true);
    }
}
