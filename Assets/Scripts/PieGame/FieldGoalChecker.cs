using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGoalChecker : MonoBehaviour
{
    [SerializeField]
    GameObject leftBoundMarker;
    [SerializeField]
    GameObject rightBoundMarker;

    [SerializeField]
    GameObject objectToCheck;


    private void Update()
    {
        Debug.DrawLine(new Vector3(leftBoundMarker.transform.position.x, leftBoundMarker.transform.position.y + 10, 0), new Vector3(leftBoundMarker.transform.position.x, leftBoundMarker.transform.position.y - 10, 0), Color.magenta);
        Debug.DrawLine(new Vector3(rightBoundMarker.transform.position.x, rightBoundMarker.transform.position.y + 10, 0), new Vector3(rightBoundMarker.transform.position.x, rightBoundMarker.transform.position.y - 10, 0), Color.magenta);
    }
    public bool IsWithinSuccessBounds()
    {
        return (objectToCheck.transform.position.x > leftBoundMarker.transform.position.x && objectToCheck.transform.position.x < rightBoundMarker.transform.position.x);
    }
}
