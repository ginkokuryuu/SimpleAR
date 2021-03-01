using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomEventHandler : MonoBehaviour
{
    public static CustomEventHandler INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }

    public event Action OnObjectDetected;
    public event Action OnObjectNotDetected;

    public void ObjectDetected()
    {
        print("Detected");
        OnObjectDetected?.Invoke();
    }

    public void ObjectNotDetected()
    {
        print("Not Detected");
        OnObjectNotDetected?.Invoke();
    }
}
