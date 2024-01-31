using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPrefs : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] readouts;
    string controlListeningFor = "none";

    [SerializeField]
    BackAndForthSlider tossControls;
    [SerializeField]
    PouringHand pourControls;

    private void Start()
    {
        CheckFirstLoad();
    }

    private void Update()
    {
        if (controlListeningFor != "none")
        {
            if (Input.anyKeyDown == true)
            {
                foreach (KeyCode pressedKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(pressedKey))
                    {
                        SaveControl(controlListeningFor, pressedKey);
                        MapControlsFromSave();
                        controlListeningFor = "none";
                    }
                }
            }
        }
    }

    public void ListenForNewControl(string controlName) //A button calls this when pressed in the remap menu
    {
        controlListeningFor = controlName;
    }

    public void MapControlsFromSave()
    {
        if (tossControls != null)
        {
            tossControls.throwKey = GetControl("Interact");
            tossControls.altThrowKey = GetControl("AltInteract");
        }
        if (pourControls != null)
        {
            pourControls.pourKey = GetControl("Interact");
            pourControls.altPourKey = GetControl("AltInteract");
        }
        //Insert other scripts to change here.
        //Question.cs has to use the GetControl function since multiple questions get created, so we can't just point to one.

        if (readouts.Length > 0)
        {
            readouts[0].text = GetControl("Interact").ToString();
            readouts[1].text = GetControl("AltInteract").ToString();
        }
    }

    void CheckFirstLoad()
    {
        if (PlayerPrefs.GetString("FirstTimeLoadingControls") != "Loaded")
        {
            ResetControls();
        }
    }

    
    public void ResetControls()
    {
        PlayerPrefs.SetString("FirstTimeLoadingControls", "Loaded");

        PlayerPrefs.SetString("ControlInteract", KeyCode.Mouse0.ToString());
        PlayerPrefs.SetString("ControlAltInteract", KeyCode.Space.ToString());

        MapControlsFromSave();
    }

    public void SaveControl(string controlName, KeyCode newKey)
    {
        PlayerPrefs.SetString("Control" + controlName, newKey.ToString());
    }

    public KeyCode GetControl(string controlName)
    {
        string keyName = PlayerPrefs.GetString("Control" + controlName);
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), keyName);
    }
}
