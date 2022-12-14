using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : Singleton<InputManager>
{
    public bool IsMovingPlayer1 { get; private set; }
    public Vector2 MoveDirectionPlayer1 { get; private set; }
    public bool IsAbilityPlayer1Pressed { get; private set; } 

    public bool IsMovingPlayer2 { get; private set; }
    public Vector2 MoveDirectionPlayer2 { get; private set; }
    public bool IsAbilityPlayer2Pressed { get; private set; }

    private bool _isPausePressed = false;

    public void MovePressedPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMovingPlayer1 = true;
            MoveDirectionPlayer1 = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            IsMovingPlayer1 = false;
            MoveDirectionPlayer1 = context.ReadValue<Vector2>();
        }
    }

    public void MovePressedPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMovingPlayer2 = true;
            MoveDirectionPlayer2 = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            IsMovingPlayer2 = false;
            MoveDirectionPlayer2 = context.ReadValue<Vector2>();
        }
    }

    public void AbilityPressedPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAbilityPlayer2Pressed = true;
        }
        else if (context.canceled)
        {
            IsAbilityPlayer2Pressed = false;
        }
    }

    public void AbilityPressedPlayer1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAbilityPlayer1Pressed = true;
        }
        else if (context.canceled)
        {
            IsAbilityPlayer1Pressed = false;
        }
    }

    public void ApplicationPause(InputAction.CallbackContext context) {
        if (context.performed)
        {
            _isPausePressed = true;
        }
        else if (context.canceled)
        {
            _isPausePressed = false;
        }
    }

    // For ButtonDown only
    public bool GetApplicationPausePressed()
    {
        bool result = _isPausePressed;
        _isPausePressed = false;
        return result;
    }
}
