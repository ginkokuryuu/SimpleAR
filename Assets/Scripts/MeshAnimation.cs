using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAnimation : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 0.5f;
    private bool isRotating = true;
    private Renderer theRenderer;
    private bool isDetected = false;

    private void Awake()
    {
        SceneLoader.INSTANCE.MeshAnimation = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theRenderer = GetComponent<Renderer>();
        CustomEventHandler.INSTANCE.OnObjectDetected += ObjectDetected;
        CustomEventHandler.INSTANCE.OnObjectNotDetected += ObjectNotDetected;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(new Vector3(0, rotateSpeed, 0), Space.Self);
        }

        if (isDetected)
        {
            if (Time.fixedTime % .5 < .2)
            {
                theRenderer.enabled = false;
            }
            else
            {
                theRenderer.enabled = true;
            }
        }
    }

    public void ManualRotate(Vector3 rotateDir)
    {
        if(isDetected)
            transform.Rotate(transform.up, rotateDir.x, Space.Self);
    }

    public void StartRotate()
    {
        if(isDetected)
            isRotating = true;
    }

    public void StopRotate()
    {
        if(isDetected)
            isRotating = false;
    }

    public void ObjectDetected()
    {
        isDetected = true;
    }

    public void ObjectNotDetected()
    {
        isDetected = false;
        theRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        CustomEventHandler.INSTANCE.OnObjectDetected -= ObjectDetected;
        CustomEventHandler.INSTANCE.OnObjectNotDetected -= ObjectNotDetected;
    }
}
