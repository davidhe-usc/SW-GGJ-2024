using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSeries : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] events;
    [SerializeField]
    float[] delays;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeAllEvents()
    {
        StartCoroutine(DelayedEvents());
    }

    IEnumerator DelayedEvents()
    {
        for (int i = 0; i < events.Length; i++)
        {
            yield return new WaitForSeconds(delays[i] /* multiplied by tempo?*/);
            events[i].Invoke(); 
        }
    }
}
