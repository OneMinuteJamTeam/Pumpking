using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public enum PlayerNumber
    {
        PlayerOne,
        PlayerTwo,
    }

    [Header("Settings")]
    [SerializeField]
    protected float moveSpeed = 5.0f;
    [SerializeField]
    protected PlayerNumber playerNumber;

    protected virtual void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsMovingPlayer1)
        {
            // Handle Input P1
            
        }
        else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsMovingPlayer2)
        {
            // Handle Input P2
        }
    }
}
