using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToSpawn;
    // Start is called before the first frame update
    
    public void Spawn()
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }

    public void VagueSpawn()
    {
        Instantiate(prefabToSpawn);
    }
}
