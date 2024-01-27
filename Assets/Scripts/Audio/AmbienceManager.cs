using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private AudioPlayCue ambA, ambB, ambC;

    private int tempoThresholdB = 3;
    private int tempoThresholdC = 4;

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
        if (Input.GetKey(KeyCode.Alpha1))
            CheckAmbienceIntensity(1);
        if (Input.GetKey(KeyCode.Alpha2))
            CheckAmbienceIntensity(2);
        if (Input.GetKey(KeyCode.Alpha3))
            CheckAmbienceIntensity(3);
        if (Input.GetKey(KeyCode.Alpha4))
            CheckAmbienceIntensity(4);
    }

    public void CheckAmbienceIntensity (int tempo)
    {
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
