using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovements
{
    private readonly CharacterConfigData _defaultCharacterConfig = Resources.Load<CharacterConfigData>("DefaultCharacterConfig");
    private readonly ICharacterMovementsInput _characterMovementsInput;

    private CharacterConfigData _currentCharacterConfig;
    private CharacterController _characterController;

    private Transform _characterTransform;
    private Vector3 _velocity;
    private float _currentSpeed;
    private bool _isFalling;

    public CharacterMovements(ICharacterMovementsInput characterMovementsInput, Transform characterTransform)
    {
        _characterMovementsInput = characterMovementsInput;
        _characterTransform = characterTransform;

        _characterController = characterTransform.GetComponent<CharacterController>();

        _currentCharacterConfig = _defaultCharacterConfig;
        _currentSpeed = _currentCharacterConfig.OnGroundSpeed;

        _characterMovementsInput.OnWalking += Walking;
        _characterMovementsInput.OnRuning += Runing;
        _characterMovementsInput.OnJumping += Jumping;
    }

    public void OnDestroy()
    {
        _characterMovementsInput.OnWalking -= Walking;
        _characterMovementsInput.OnRuning -= Runing;
        _characterMovementsInput.OnJumping -= Jumping;
    }

    public void Tick(float time)
    {
        Gravity(time);
        Movements(time);

        _characterMovementsInput.Tick();
    }

    public void SetCharacterConfig(CharacterConfigData characterConfigData)
    {
        _currentCharacterConfig = characterConfigData;
    }

    public void SetDefaultChatacterConfig()
    {
        _currentCharacterConfig = _defaultCharacterConfig;
    }

    private void Movements(float time)
    {
        _characterController.Move(_velocity * time);
    }

    private void Walking(Vector2 inputValue)
    {
        var direction = _characterTransform.right * inputValue.x + _characterTransform.forward * inputValue.y;
        var speed = _currentSpeed * Time.fixedDeltaTime;

        _velocity = new Vector3(direction.x * speed, _velocity.y, direction.z * speed);
    }

    private void Runing(bool isActive)
    {
        if (!_characterController.isGrounded) return;
        _currentSpeed = isActive && !_isFalling ? _currentCharacterConfig.OnRunSpeed : _currentCharacterConfig.OnGroundSpeed;
    }

    private void Jumping()
    {
        if (!_characterController.isGrounded) return;
        _velocity.y = _currentCharacterConfig.JumpPower;
    }

    private void Gravity(float time)
    {
        if (_characterController.isGrounded && _velocity.y < 0) _velocity.y = -1;
        _velocity.y -= _currentCharacterConfig.GravityValue * time;

        if(!_characterController.isGrounded && _characterController.velocity.y < -1) _isFalling = true;
        else _isFalling = false;

        if (_isFalling) _currentSpeed = Mathf.Lerp(_currentSpeed, _currentCharacterConfig.InFlySpeed, _currentCharacterConfig.FallingValue * Time.deltaTime);
    } 
}
