using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringHand : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D handRigidbody;
    [SerializeField]
    private Rigidbody2D hoseRigidbody;
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private PouringGame gameController;
    [SerializeField]
    private float handSpeed;

    private float ballCooldown = 0;
    public float ballCooldownMax;
    [SerializeField]
    private bool canMove = false;

    private float tempo;
    [SerializeField]
    private float tempoVelocityFactor;
    private float tempoVelocityMod;

    public float velocityMod;

    private AudioPlayCue audioPlayerLoop;
    private bool canPlayAudio = true;

    void Start()
    {
        gameController.m_StartGame.AddListener(EnableMovement);
        gameController.m_EndGame.AddListener(DisableMovement);
        tempo = gameController.tempo;
        tempoVelocityMod = tempo * tempoVelocityFactor;

        audioPlayerLoop = GetComponent<AudioPlayCue>();
        //audioPlayerLoop.Play(true);
    }

    void EnableMovement() {
        canMove = true;
    }

    void DisableMovement() {
        canMove = false;
        handRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        hoseRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 2.0f;
            Vector3 targetInWorld = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 direction = targetInWorld - handRigidbody.transform.position;
            handRigidbody.MovePosition(handRigidbody.transform.position + (direction.normalized * handSpeed));

            if (ballCooldown == 0 && gameController.numBalls < gameController.maxBalls && (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))) {
                CreateBall();
                ballCooldown = ballCooldownMax;

                if(canPlayAudio == true)
                {
                    audioPlayerLoop.Play(false);
                    canPlayAudio = false;
                }
            }
            else
            {
                audioPlayerLoop.Stop(false);
                canPlayAudio = true;
            }
            ballCooldown = Mathf.Max(ballCooldown - Time.deltaTime, 0);
        }
    }

    void CreateBall()
    {
        if (!gameController.isDogVersion)
        {
            gameController.numBalls++;
        }
        GameObject newBall = Instantiate(ball, spawn.position, new Quaternion(0, 0, Random.Range(-1f, 1f), 1));
        newBall.GetComponent<SpriteRandomizer>().SetRandomSprite();
        newBall.GetComponent<Rigidbody2D>().AddForce(hoseRigidbody.transform.up * (velocityMod + tempoVelocityMod));
    }
}
