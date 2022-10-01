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

    private Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsMovingPlayer1)
        {
            // Handle Input P1
            _rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer1.y * moveSpeed);
            
        }
        else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsMovingPlayer2)
        {
            // Handle Input P2
            _rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer2.y * moveSpeed);
        }
    }
}
