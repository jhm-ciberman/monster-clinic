using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera introCamera;
    public Camera gameplayCamera;


    public void Start()
    {
        this.introCamera.enabled = false;
        this.gameplayCamera.enabled = false;
    }
}
