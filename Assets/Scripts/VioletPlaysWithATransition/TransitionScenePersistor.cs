using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScenePersistor : MonoBehaviour
{
    [SerializeField]
    Animator transitionAnimator;
    [SerializeField]
    string exitAnimationName;
    [SerializeField]
    float lifetimeOnExit;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        Debug.Log("do a thing, a scene just loaded!");
        transitionAnimator.Play(exitAnimationName);
        //Destroy(this.gameObject, lifetimeOnExit); 
    }
}
