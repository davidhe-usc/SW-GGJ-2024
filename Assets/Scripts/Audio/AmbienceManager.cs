using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private AudioPlayCue ambA, ambB, ambC, ambD;

    private int tempoThresholdB = 3;
    private int tempoThresholdC = 6;
    private int tempoThresholdD = 9;

    public static AmbienceManager instance;

    private void Awake()
    {
        AmbienceManager[] tms = FindObjectsOfType<AmbienceManager>();

        if (tms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
