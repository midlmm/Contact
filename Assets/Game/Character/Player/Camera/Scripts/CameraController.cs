using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;
    [SerializeField] private float _maxAngleY;

    [SerializeField] private Transform _enemy;

    private Transform _currentEnemy;
    private float _currentRotationY;

    private void Awake()
    {
        _currentEnemy = _enemy;
    }

    public void RotateEnemy(float angleX)
    {
        _currentEnemy.Rotate(_currentEnemy.up * angleX * sensitivity);
    }

    public void RotateCamera(float angleY)
    {
        _currentRotationY -= angleY * sensitivity;

        _currentRotationY = Mathf.Clamp(_currentRotationY, -_maxAngleY, _maxAngleY);

        transform.localRotation = Quaternion.Euler(_currentRotationY, 0, 0);
    }
}
