using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private float knockbackChance;
    [SerializeField]
    private float tempoKnockbackMod;
    [SerializeField]
    private BoxCollider2D activeHandRegion;

    private Vector3 minHandPosition;

    public KeyCode pourKey = KeyCode.Mouse0;
    public KeyCode altPourKey = KeyCode.Space;

    void Start()
    {
        gameController.m_StartGame.AddListener(EnableMovement);
        gameController.m_EndGame.AddListener(DisableMovement);
        tempo = gameController.tempo;
        tempoVelocityMod = tempo * tempoVelocityFactor;
        knockbackChance = ((tempo / 100f) / 2f) + 0.1f;
        minHandPosition = activeHandRegion.bounds.min;

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
            Vector3 intended = handRigidbody.transform.position + (direction.normalized * handSpeed);

            handRigidbody.MovePosition(new Vector3(Mathf.Max(minHandPosition.x, intended.x), Mathf.Max(minHandPosition.y, intended.y), intended.z));

            if (ballCooldown == 0 && gameController.numBalls < gameController.maxBalls && (Input.GetKey(pourKey) || Input.GetKey(altPourKey) && !EventSystem.current.IsPointerOverGameObject())) {
                CreateBall();
                ballCooldown = ballCooldownMax;

                if(canPlayAudio == true)
                {
                    if(audioPlayerLoop!=null)
                        audioPlayerLoop.Play(false);
                    canPlayAudio = false;
                }
            }
            else
            {
                if (audioPlayerLoop != null)
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
        if (Random.Range(0f, 1f) < knockbackChance)
        {
            hoseRigidbody.AddForce(new Vector2(0, tempo * tempoKnockbackMod));
        }
        GameObject newBall = Instantiate(ball, spawn.position, new Quaternion(0, 0, Random.Range(-1f, 1f), 1));
        newBall.GetComponent<SpriteRandomizer>().SetRandomSprite();
        newBall.GetComponent<Rigidbody2D>().AddForce(hoseRigidbody.transform.up * (velocityMod + tempoVelocityMod));
    }
}
