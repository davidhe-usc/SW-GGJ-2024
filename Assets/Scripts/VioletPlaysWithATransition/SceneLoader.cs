using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName.Equals("Title") && TempoManager.instance != null)
        {
            TempoManager.instance.Reset();
        }

        if (sceneName.Equals("Outro") && MusicManager.instance != null)
            MusicManager.instance.ChangeMusic(MusicManager.instance.musicCueEnding);

        SceneManager.LoadScene(sceneName);


    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
