using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        if (!pause)
        {
            //Debug.Log("Pause");
            pause = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            //Debug.Log("Unpause");
            pause = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void ExitGame()
    {
        TempoManager.instance.Reset();
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(TransitionThenLoadScene("Title", 1));
    }

    IEnumerator TransitionThenLoadScene(string sceneName, float delay)
    {
        FindObjectOfType<SimpleSpawner>().VagueSpawn();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
