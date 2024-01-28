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
        if (transitionAnimator == null)
        {
            transitionAnimator = GetComponent<Animator>(); //This throws an error saying the Persistor is gone.
        }
        if (transitionAnimator != null)
        {
            transitionAnimator.Play(exitAnimationName);
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //Destroy(this.gameObject, lifetimeOnExit); 
    }
}
