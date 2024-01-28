using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public List<Sprite> sprites;

    public void SetRandomSprite() {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 3)];
    }
}
