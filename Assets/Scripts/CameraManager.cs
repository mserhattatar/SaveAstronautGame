using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{ 
    private bool _moveCamera;
    public float forwardSpeed;

    private void Start()
    {
        CanvasManager.GameStartDelegate += MoveCameraSetActive;
        CanvasManager.GameOverDelegate += MoveCameraSetPassive;
        CanvasManager.GameResetDelegate += MoveCameraSetPassive;
    }

    private void FixedUpdate()
    {
        if (_moveCamera)
        {
            transform.position += Vector3.up * (forwardSpeed * Time.deltaTime);
        }
    }

    private void MoveCameraSetActive()
    {
        _moveCamera = true;
    }
    private void MoveCameraSetPassive()
    {
        _moveCamera = false;
    }
}
