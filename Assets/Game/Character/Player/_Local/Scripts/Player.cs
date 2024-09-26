using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterMovements PlayerMovements { get; private set; }

    [SerializeField] private Transform _playerTransform;
    private PlayerMovementsInput _playerMovementsInput;

    private void Awake()
    {
        Initialized();
    }

    public void Initialized()
    {
        _playerMovementsInput = new PlayerMovementsInput();
        PlayerMovements = new CharacterMovements(_playerMovementsInput, _playerTransform);

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        PlayerMovements.Tick(Time.fixedDeltaTime);
    }
}
