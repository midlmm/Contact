using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Character Config")]
public class CharacterConfigData : ScriptableObject
{
    public float OnGroundSpeed => _onGroundSpeed;
    public float OnRunSpeed => _onRunSpeed;
    public float InFlySpeed => _inFlySpeed;
    public float GravityValue => _gravityValue;
    public float JumpPower => _jumpPower;
    public float FallingValue => _fallingValue;

    [SerializeField] private float _onGroundSpeed;
    [SerializeField] private float _onRunSpeed;
    [SerializeField] private float _inFlySpeed;
    [Space]
    [SerializeField] private float _gravityValue;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _fallingValue;
}
