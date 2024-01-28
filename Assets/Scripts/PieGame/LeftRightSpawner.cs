using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToSpawn;
    [SerializeField]
    GameObject leftSpawnPoint;
    [SerializeField]
    GameObject rightSpawnPoint;
    [SerializeField]
    GameObject midpoint;
    [SerializeField]
    GameObject pie;
    // Start is called before the first frame update 

    public void SpawnToSides()
    {
        if (pie.transform.position.x < midpoint.transform.position.x)
        {
            Instantiate(prefabToSpawn, leftSpawnPoint.transform.position, Quaternion.identity);
        } else {
            Instantiate(prefabToSpawn, rightSpawnPoint.transform.position, Quaternion.identity);
        } 

    }
}
