using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogContinueKeyActivator : MonoBehaviour
{
    public KeyCode continueKey = KeyCode.Mouse0;
    public KeyCode altContinueKey = KeyCode.Space;

    [SerializeField]
    Button continueButton;

    //ControlPrefs controlPrefs;
    // Start is called before the first frame update
    void Start()
    {
        if (continueButton == null)
        {
            continueButton = gameObject.GetComponent<Button>();
        }
        /*controlPrefs = FindObjectOfType<ControlPrefs>();
        if (controlPrefs != null)
        {
            continueKey = controlPrefs.GetControl("Interact");
            altContinueKey = controlPrefs.GetControl("AltInteract");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if ((continueKey != KeyCode.Mouse0 && Input.GetKeyDown(continueKey)) || (altContinueKey != KeyCode.Mouse0 && Input.GetKeyDown(altContinueKey)))
        {
            if (continueButton.enabled == true && continueButton.interactable == true)
            {
                continueButton.onClick.Invoke();
            }
        }
    }
}
