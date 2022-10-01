using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerNumber
    {
        PlayerOne,
        PlayerTwo,
    }

    [Header("Settings")]
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private PlayerNumber playerNumber;

    private void Update()
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
