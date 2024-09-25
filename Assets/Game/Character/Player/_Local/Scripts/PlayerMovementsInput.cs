using System;
using UnityEngine;

public class PlayerMovementsInput : ICharacterMovementsInput
{
    public event Action<Vector2> OnWalking;
    public event Action<bool> OnRuning;
    public event Action OnJumping;

    public void Tick()
    {
        WalkInput();
        JumpInput();
        RunInput();
    }

    private void WalkInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        var direction = new Vector2(x, y);

        OnWalking?.Invoke(direction);
    }

    private void JumpInput()
    {
        if (Input.GetKey(KeyCode.Space)) OnJumping?.Invoke();
    }

    private void RunInput()
    {
        if (Input.GetKey(KeyCode.LeftShift)) OnRuning?.Invoke(true);
        else OnRuning?.Invoke(false);
    }
}
