using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab;
    [SerializeField]
    Vector2 xInitialVelocityRange;
    [SerializeField]
    Vector2 yInitialVelocityRange;
    [SerializeField]
    float critChance;
    [SerializeField]
    float critModifier;
    [SerializeField]
    GameObject throwObject;
    // Start is called before the first frame update
    

    public void SpawnABall(float xPosition)
    {
        GameObject newBall = Instantiate(ballPrefab, new Vector3(xPosition, transform.position.y, 0), Quaternion.identity);
        float xSpeed = Random.Range(xInitialVelocityRange.x, xInitialVelocityRange.y);
        float ySpeed = Random.Range(yInitialVelocityRange.x, yInitialVelocityRange.y);
        if (Random.Range(0.00f, 1.00f) < critChance)
        {
            ySpeed = ySpeed * critModifier;
            //Debug.Log("Crit! New speed of " + ySpeed);
        }
        newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
    }

    public void SpawnBallAtThrow()
    {
        SpawnABall(throwObject.transform.position.x);
    }
}
