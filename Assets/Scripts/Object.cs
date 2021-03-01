using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 20f;
    [SerializeField] private float scaleRate = 0.01f;
    private Vector3 scaleVector = new Vector3();
    private bool isDetected = false;

    // Start is called before the first frame update
    void Awake()
    {
        SceneLoader.INSTANCE.ActiveObject = this;
    }

    private void Start()
    {
        CustomEventHandler.INSTANCE.OnObjectDetected += ObjectDetected;
        CustomEventHandler.INSTANCE.OnObjectNotDetected += ObjectNotDetected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scale(float changeValue)
    {
        if (isDetected)
        {
            float changeRate = changeValue * scaleRate;
            float scaleValue = Mathf.Clamp(transform.localScale.x - changeRate, minScale, maxScale);
            scaleVector.Set(scaleValue, scaleValue, scaleValue);
            transform.localScale = scaleVector;
        }
    }

    public void ObjectDetected()
    {
        isDetected = true;
    }

    public void ObjectNotDetected()
    {
        isDetected = false;
    }

    private void OnDestroy()
    {
        CustomEventHandler.INSTANCE.OnObjectDetected -= ObjectDetected;
        CustomEventHandler.INSTANCE.OnObjectNotDetected -= ObjectNotDetected;
    }
}
