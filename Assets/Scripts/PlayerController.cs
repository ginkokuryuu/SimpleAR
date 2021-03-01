using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 prevMousePos = new Vector3();
    private Vector3 mousePosition;
    private MeshAnimation meshAnimation;
    private Object theObject;

    private void Awake()
    {
        SceneLoader.INSTANCE.PlayerController = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        meshAnimation = SceneLoader.INSTANCE.MeshAnimation;
        theObject = SceneLoader.INSTANCE.ActiveObject;
        CustomEventHandler.INSTANCE.ObjectNotDetected();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            meshAnimation.StopRotate();
            prevMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            Vector3 direction = prevMousePos - mousePosition;

            meshAnimation.ManualRotate(direction);

            prevMousePos = Input.mousePosition;
        }
        else
        {
            meshAnimation.StartRotate();
        }

        if(Input.touchCount == 2)
        {
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            Vector3 prevOnePos = touchOne.position - touchOne.deltaPosition;
            Vector3 prevTwoPos = touchTwo.position - touchTwo.deltaPosition;

            float prevDistance = Vector3.Distance(prevOnePos, prevTwoPos);
            float distanceNow = Vector3.Distance(touchOne.position, touchTwo.position);

            float difference = prevDistance - distanceNow;

            theObject.Scale(difference);
        }

        theObject.Scale(Input.GetAxis("Mouse ScrollWheel") * 100);
    }
}
