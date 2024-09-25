using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerInput : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
    private void Update()
    {
        _cameraController.RotateEnemy(Input.GetAxis("Mouse X"));
        _cameraController.RotateCamera(Input.GetAxis("Mouse Y"));
    }
}
