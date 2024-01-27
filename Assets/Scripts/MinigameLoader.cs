using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class MinigameLoader:MonoBehaviour
{
    [YarnCommand("load_minigame")]
    public static void LoadMinigame(string gameName)
    {
        SceneManager.LoadScene(gameName);
    }

    public void EndMinigame()
    {
        SceneManager.LoadScene("YarnTest");
    }
}
