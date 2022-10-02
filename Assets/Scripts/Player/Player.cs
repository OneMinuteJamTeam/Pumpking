using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour
{
    public enum PlayerNumber
    {
        PlayerOne,
        PlayerTwo,
    }

    public bool IsEscaping { get => isEscaping; }
    public bool CanReadInput { get; set; }

    [Header("Settings")]
    [SerializeField]
    protected float moveSpeed = 5.0f;
    [SerializeField]
    protected float rotationSpeed = 100.0f;
    [SerializeField]
    protected float abilityCooldown = 2.0f;
    [SerializeField]
    protected bool isEscaping = false;
    [SerializeField]
    protected PlayerNumber playerNumber;

    protected bool canUseAblity = true;

    protected Rigidbody rb;
    protected Vector3 lastSpeedDirection;

    private float _originalMoveSpeed;
    private bool _collideOnce = false;

    public virtual void SetIsEscaping(bool isEscaping)
    {
        this.isEscaping = isEscaping;
    }

    public PlayerNumber GetPlayerNumber() => playerNumber;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        CanReadInput = true;
        canUseAblity = true;
        _originalMoveSpeed = moveSpeed;
    }

    protected virtual void Update()
    {
        HandleInput();
        HandleRotation();
    }

    protected abstract void UseAbility();

    protected virtual void HandleInput()
    {
        if (CanReadInput) {
            if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsMovingPlayer1) 
            {
                // Handle Input P1
                rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer1.y * moveSpeed);

                lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x, 0, InputManager.Instance.MoveDirectionPlayer1.y);
            }
            else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsMovingPlayer2)
            {
                // Handle Input P2
                rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer2.y * moveSpeed);

                lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x, 0, InputManager.Instance.MoveDirectionPlayer2.y);
            }
            else rb.velocity = Vector3.zero;

            HandleAbility();
        }
    }

    private void HandleRotation()
    {
        if (lastSpeedDirection == Vector3.zero)
            return;
        Quaternion toRot = Quaternion.LookRotation(lastSpeedDirection, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotationSpeed * Time.deltaTime);
    }

    private void HandleAbility()
    {
        if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsAbilityPlayer1Pressed && canUseAblity) 
        {
            canUseAblity = false;
            UseAbility();
            StartCoroutine(COStartCooldown());
        }
        else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsAbilityPlayer2Pressed && canUseAblity)
        {
            canUseAblity = false;
            UseAbility();
            StartCoroutine(COStartCooldown());
        }
    }

    private IEnumerator COStartCooldown()
    {
        yield return new WaitForSeconds(abilityCooldown);
        canUseAblity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Player") && !_collideOnce)
        {
            _collideOnce = true;
            if (!IsEscaping)
            {
                Debug.Log("POINT GIVEN FROM COLLISION");
                GameController.Instance.GivePoint(((int)playerNumber));
            }
                
        }
    }

    #region SpeedBoost Handling

    private Coroutine endSpeedBoostRef;
    public void BoostSpeed(float _boostAmmount, float _duration) 
    {
        if (moveSpeed == _originalMoveSpeed)
            moveSpeed += _boostAmmount;
        if (endSpeedBoostRef != null) StopCoroutine(endSpeedBoostRef);
        endSpeedBoostRef = StartCoroutine(endSpeedBoost(_duration));

    }
    private IEnumerator endSpeedBoost(float _duration) 
    {
        Debug.Log("boost speed picked, ending in " + _duration + " seconds");
        yield return new WaitForSeconds(_duration);
        moveSpeed = _originalMoveSpeed;
        Debug.Log("boost speed ended");
    }
    #endregion
}
