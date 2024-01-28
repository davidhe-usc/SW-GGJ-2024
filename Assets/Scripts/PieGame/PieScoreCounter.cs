using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PieScoreCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreCounter;
    [SerializeField]
    string scoreMiddleText;
    [SerializeField]
    PieWinCheck pieWinCheck;
    [SerializeField]
    GameObject particleSpawnLocation;
    [SerializeField]
    GameObject successParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText(); 
    }

    public void UpdateText()
    {
        scoreCounter.text = pieWinCheck.GetCurrentSuccesses() + scoreMiddleText + pieWinCheck.GetSuccessesNeeded();
    }

    public void UpdateTextWithFlourish()
    {
        if (successParticlePrefab != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(particleSpawnLocation.GetComponent<RectTransform>().transform.position);
            worldPosition.z = 0;
            Instantiate(successParticlePrefab, worldPosition, Quaternion.identity);
        }
        UpdateText();
    }
}
