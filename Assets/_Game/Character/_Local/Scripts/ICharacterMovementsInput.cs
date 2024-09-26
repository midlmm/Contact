using System;
using UnityEngine;

public interface ICharacterMovementsInput 
{
    public event Action<Vector2> OnWalking;
    public event Action<bool> OnRuning;
    public event Action OnJumping;

    public abstract void Tick();
}
