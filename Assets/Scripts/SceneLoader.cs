using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader INSTANCE;
    private MeshAnimation meshAnimation;
    private PlayerController playerController;
    private Object activeObject;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public MeshAnimation MeshAnimation { get => meshAnimation; set => meshAnimation = value; }
    public Object ActiveObject { get => activeObject; set => activeObject = value; }

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
