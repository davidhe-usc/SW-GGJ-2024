using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField]
    float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    } 
}
