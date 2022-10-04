using System.Collections;
using System.Collections.Generic;
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
    public bool CanMove { get; set; }

    [Header("References")]
    [SerializeField]
    private GameObject crownObj;

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
    [SerializeField]
    protected Color coolDownBarColor;

    [Header("Dependencies")]
    [SerializeField] 
    Animator anim;


    protected bool canUseAbility = true;

    protected Rigidbody rb;
    protected Vector3 lastSpeedDirection;

    private float _originalMoveSpeed;
    private bool _collideOnce = false;

    protected MyUIBar coolDownBar;

    public virtual void SetIsEscaping(bool isEscaping)
    {
        this.isEscaping = isEscaping;
    }

    public PlayerNumber GetPlayerNumber() => playerNumber;

    protected virtual void Awake()
    {
        CanReadInput = false;
        canUseAbility = true;
        CanMove = true;
        rb = GetComponent<Rigidbody>();
        coolDownBar = GameUIManager.Instance.GetCooldownBar(playerNumber);
    }

    protected virtual void Start()
    {
        _originalMoveSpeed = moveSpeed;
        coolDownBar.SetMaxFill(abilityCooldown);
        coolDownBar.SetColor(coolDownBarColor);
    }

    protected virtual void Update()
    {
        HandleInput();
        HandleRotation();
    }

    protected abstract void UseAbility();


    // PLAYER CANNOT ROTATE WHEN TRAPPED
    //protected virtual void HandleInput()
    //{
    //    if (CanReadInput) {

    //        if (CanMove) {
    //            if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsMovingPlayer1) {
    //                // Handle Input P1
    //                rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer1.y * moveSpeed);
    //                lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x, 0, InputManager.Instance.MoveDirectionPlayer1.y);

    //                RotateSpeedWithWalls();

    //            }
    //            else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsMovingPlayer2) {
    //                // Handle Input P2
    //                rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer2.y * moveSpeed);

    //                lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x, 0, InputManager.Instance.MoveDirectionPlayer2.y);

    //                RotateSpeedWithWalls();
    //            }
    //            else rb.velocity = Vector3.zero;
    //        }
    //        else rb.velocity = Vector3.zero;
    //        HandleAbility();
    //    }
    //}
    
    // PLAYER CAN ROTATE WHEN TRAPPED
    protected virtual void HandleInput() {
        if (CanReadInput) {

                // Handle Input P1
                if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsMovingPlayer1) {
<<<<<<< Updated upstream

                    lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x, 0, InputManager.Instance.MoveDirectionPlayer1.y);
                    if (CanMove) {
                        rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer1.y * moveSpeed);
                        RotateSpeedWithWalls();
                    }
                    else rb.velocity = Vector3.zero;
=======
                    // Handle Input P1
                    rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer1.y * moveSpeed);
                    lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x, 0, InputManager.Instance.MoveDirectionPlayer1.y);
                    
                    RotateSpeedWithWalls();
>>>>>>> Stashed changes

                }
                // Handle Input P2
                else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsMovingPlayer2) {
                    lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x, 0, InputManager.Instance.MoveDirectionPlayer2.y);

                    if (CanMove) {
                        rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer2.y * moveSpeed);
                        RotateSpeedWithWalls();
                    }
                    else rb.velocity = Vector3.zero;
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
        if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsAbilityPlayer1Pressed && canUseAbility) 
        {
            canUseAbility = false;
            UseAbility();
            StartCoroutine(COStartCooldown());
        }
        else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsAbilityPlayer2Pressed && canUseAbility)
        {
            canUseAbility = false;
            UseAbility();
            StartCoroutine(COStartCooldown());
        }
    }

    private IEnumerator COStartCooldown()
    {
        float charge = 0;
        float startTime = Time.time;
        while (charge < abilityCooldown){
            charge = Time.time - startTime;
            coolDownBar.SetFill(charge);
            yield return null;
        }
        //yield return new WaitForSeconds(abilityCooldown);
        canUseAbility = true;
    }

    Vector3? hitWallNormal = null;
    private void OnCollisionStay(Collision collision) {

        if(collision.gameObject.tag == "Wall")
            hitWallNormal = collision.contacts[0].normal;
    }
    private void OnCollisionExit(Collision collision) {
        hitWallNormal = null;
    }
    private void RotateSpeedWithWalls() {
        if (hitWallNormal != null) {
            if(Vector3.Angle((Vector3)hitWallNormal, rb.velocity) > 90) {

                float originalMagnitude = rb.velocity.magnitude;

                lastSpeedDirection = Vector3.ProjectOnPlane(lastSpeedDirection, (Vector3)hitWallNormal);
                rb.velocity = Vector3.ProjectOnPlane(rb.velocity, (Vector3)hitWallNormal);
                rb.velocity = rb.velocity.normalized * originalMagnitude;
            }
        }
    }

    #region SpeedBoost Handling

    private Coroutine endSpeedBoostRef;
    public void BoostSpeed(float _boostAmmount, float _duration) 
    {
        crownObj.SetActive(true);
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
        crownObj.SetActive(false);
        Debug.Log("boost speed ended");
    }
    #endregion
} 
