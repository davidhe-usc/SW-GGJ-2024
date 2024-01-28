using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPouringBall : MonoBehaviour
{

    private Rigidbody2D rb;
    private Collider2D collider;
    private AudioPlayOneShot audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider= GetComponent<Collider2D>();
        audioPlayer.GetComponent<AudioPlayOneShot>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > 1)
            audioPlayer.Play();
    }
}
