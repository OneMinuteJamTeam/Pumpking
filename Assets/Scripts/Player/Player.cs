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

    public enum eRole {
        Chaser,
        Escapee
    }

    //public bool IsEscaping { get => isEscaping; }
    public eRole Role { get { return role; } }
    public bool CanUseAbility { get; set; }
    public bool CanMove { get; set; }
    public bool IsSpeedControlled { get; set; }

    public static string P1_ESCAPEE_KEY = "P1ESCAPEE_KEY";
    public static string P2_ESCAPEE_KEY = "P2ESCAPEE_KEY";

    [Header("References")]
    [SerializeField]
    private GameObject crownObj;
    [SerializeField]
    private Light playerLight;

    [Header("Settings")]
    [SerializeField]
    protected float moveSpeed = 5.0f;
    [SerializeField]
    protected float rotationSpeed = 100.0f;
    [SerializeField]
    protected float abilityCooldown = 2.0f;
    //[SerializeField]
    //protected bool isEscaping = false;
    [SerializeField]
    protected PlayerNumber playerNumber;
    [SerializeField] 
    protected GameObject model;
    protected eRole role;

    protected Rigidbody rb;
    protected Vector3 lastSpeedDirection;

    private float _originalMoveSpeed;

    protected MyUIBar coolDownBar;

    protected Animator animator;

    public virtual void SetRole(eRole _role)
    {
        role = _role;
        if (_role == eRole.Escapee) {
            playerLight.color = ColorsManager.Instance.EscapeeLightColor;
            coolDownBar.SetColor(ColorsManager.Instance.EscapeeUiColor);
        }
        else
        {
            playerLight.color = ColorsManager.Instance.ChaserLightColor;
            coolDownBar.SetColor(ColorsManager.Instance.ChaserUiColor);
        }
    }

    public void SwapRole() {
        if(role == eRole.Escapee) SetRole(eRole.Chaser);
        else SetRole(eRole.Escapee);
    }

    public PlayerNumber GetPlayerNumber() => playerNumber;

    protected virtual void Awake()
    {
        CanUseAbility = false;  
        CanMove = true;
        IsSpeedControlled = true;

        rb = GetComponent<Rigidbody>();
        coolDownBar = GameUIManager.Instance.GetCooldownBar(playerNumber);
        animator = model.GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        _originalMoveSpeed = moveSpeed;
        coolDownBar.SetMaxFill(abilityCooldown);
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleAbility();
    }

    protected abstract void UseAbility();
    
    // PLAYER CAN ROTATE WHEN TRAPPED
    protected virtual void HandleMovement() {

        //Debug.Log(playerNumber+": is speed controlled: "+IsSpeedControlled);

        if (IsSpeedControlled) {
            if (CanMove) {
                if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsMovingPlayer1) {
                    lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x, 0, InputManager.Instance.MoveDirectionPlayer1.y);
                    animator.SetBool("Moving", true);
                    rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer1.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer1.y * moveSpeed);
                    RotateSpeedWithWalls();
                }
                else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsMovingPlayer2) {
                    lastSpeedDirection = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x, 0, InputManager.Instance.MoveDirectionPlayer2.y);
                    animator.SetBool("Moving", true);
                    rb.velocity = new Vector3(InputManager.Instance.MoveDirectionPlayer2.x * moveSpeed, 0.0f, InputManager.Instance.MoveDirectionPlayer2.y * moveSpeed);
                    RotateSpeedWithWalls();
                }
                else {
                    animator.SetBool("Moving", false);
                    rb.velocity = Vector3.zero;
                }
            }
            else {
                animator.SetBool("Moving", false);
                rb.velocity = Vector3.zero;
            }
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
        if (playerNumber == PlayerNumber.PlayerOne && InputManager.Instance.IsAbilityPlayer1Pressed && CanUseAbility) 
        {
            CanUseAbility = false;
            UseAbility();
            StartCoroutine(COStartCooldown());
        }
        else if (playerNumber == PlayerNumber.PlayerTwo && InputManager.Instance.IsAbilityPlayer2Pressed && CanUseAbility)
        {
            CanUseAbility = false;
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
        CanUseAbility = true;
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
