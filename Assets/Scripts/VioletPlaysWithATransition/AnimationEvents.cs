using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]
    UnityEvent[] animationEvents;
    // Start is called before the first frame update
    public void CallAnimationEvent(int index)
    {
        animationEvents[index].Invoke();
    }
}
