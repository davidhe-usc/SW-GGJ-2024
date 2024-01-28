using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPaletteSetter : MonoBehaviour
{
    [SerializeField]
    Image[] lightImages;
    [SerializeField]
    Image[] secondaryLightImages;
    [SerializeField]
    Image[] darkImages;
    [SerializeField]
    Image[] secondaryDarkImages;
    [SerializeField]
    Color[] lightColors;
    [SerializeField]
    Color[] secondaryLightColors;
    [SerializeField]
    Color[] darkColors;
    [SerializeField]
    Color[] secondaryDarkColors;
    [SerializeField]
    int startingPalette;
    // Start is called before the first frame update
    void Start()
    {
        if (startingPalette < lightColors.Length)
        {
            SetPalette(startingPalette);
        } else
        {
            SetRandomPalette();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPalette(int paletteID)
    {
        for (int i = 0; i < lightImages.Length; i++)
        {
            lightImages[i].color = lightColors[paletteID];
        }
        for (int i = 0; i < secondaryLightImages.Length; i++)
        {
            secondaryLightImages[i].color = secondaryLightColors[paletteID];
        }
        for (int i = 0; i < darkImages.Length; i++)
        {
            darkImages[i].color = darkColors[paletteID];
        }
        for (int i = 0; i < secondaryDarkImages.Length; i++)
        {
            secondaryDarkImages[i].color = secondaryDarkColors[paletteID];
        }
    }

    public void SetRandomPalette()
    {
        SetPalette(Random.Range(0, lightColors.Length)); 
    }
}
