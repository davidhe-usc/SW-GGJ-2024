using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieIconSuccessState : MonoBehaviour
{
    [SerializeField]
    Sprite neutralSprite;
    [SerializeField]
    Sprite successSprite;
    [SerializeField]
    Sprite failSprite;
    [SerializeField]
    Image image;

    [SerializeField]
    GameObject successParticlePrefab;
    [SerializeField]
    GameObject failParticlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSuccess()
    {
        image.sprite = successSprite;
        /*Vector3 worldPosition = Camera.main.ScreenToWorldPoint(image.gameObject.GetComponent<RectTransform>().transform.position);
        worldPosition.z = 0;
        Instantiate(successParticlePrefab, worldPosition, Quaternion.identity);*/
    }

    public void ShowFail()
    {
        image.sprite = failSprite;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(image.gameObject.GetComponent<RectTransform>().transform.position);
        worldPosition.z = 0;
        Instantiate(failParticlePrefab, worldPosition, Quaternion.identity);
    }
} 
