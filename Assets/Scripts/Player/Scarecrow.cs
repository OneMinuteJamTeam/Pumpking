using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class Scarecrow : Player
{
    public float AbilityRange { get => abilityRange; }
    public bool HasTarget { get=> _targetInRange; }
    public Player AbilityTarget { get=> _abilityTarget; }

    [Header("Scarecrow reference")]
    [SerializeField]
    private GameObject abilityVisual;

    [Header("Scarecrow Settings")]
    [SerializeField]
    private float abilityRange = 3.0f;
    [SerializeField]
    private float pushForce = 5.0f;
    [SerializeField]
    private float pullForce = 5.0f;
    [SerializeField]
    private float abilityDuration = 2.0f;
    [SerializeField]
    private LayerMask obastaclesLayer;


    private Player _abilityTarget = null;
    private bool _targetInRange = false;

    public override void SetIsEscaping(bool isEscaping)
    {
        base.SetIsEscaping(isEscaping);
        PlayerPrefs.SetInt("ScarecrowEscaping", isEscaping ? 1 : 0);
    }

    protected override void Awake()
    {
        base.Awake();

        FindTarget();
        abilityVisual.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        
        CheckAbilityRange();
    }

    protected override void UseAbility()
    {
        if(!HasTarget)
        {
            canUseAbility = true;
            return;
        }
        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Ability used");

        CanReadInput = false;

        // Choose Pull or Push
        if (IsEscaping)
            PushAbility();
        else
            PullAbility();

    }

    private void CheckAbilityRange()
    {
        if (_abilityTarget == null)
        {
            FindTarget();
        }
            
        if(Vector3.Distance(_abilityTarget.transform.position, this.transform.position) < abilityRange)
        {
            RaycastHit hit;
            Vector3 origin = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Debug.DrawRay(origin, (_abilityTarget.transform.position - origin) * Vector3.Distance(_abilityTarget.transform.position, this.transform.position), Color.red);
            if (Physics.Raycast(origin, (_abilityTarget.transform.position - origin), out hit, Vector3.Distance(_abilityTarget.transform.position, this.transform.position), obastaclesLayer))
            {
                Debug.Log(hit.collider.gameObject);
                return;
            }
            _targetInRange = true;
            abilityVisual.SetActive(true);
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Target InRange");
        }
        else
        {
            _targetInRange = false; 
            abilityVisual.SetActive(false);
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Target Not InRange");
        }
    }

    private void PushAbility()
    {
        Vector3 dir = (_abilityTarget.transform.position - transform.position).normalized;

        animator.SetTrigger("Push");
        _abilityTarget.CanReadInput = false;
        _abilityTarget.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _abilityTarget.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
        StartCoroutine(COStartAbilityEffectTimer());
    }

    private void PullAbility()
    {
        Vector3 dir = (transform.position - _abilityTarget.transform.position).normalized;

        animator.SetTrigger("Pull");
        _abilityTarget.CanReadInput = false;
        _abilityTarget.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _abilityTarget.GetComponent<Rigidbody>().AddForce(dir * pullForce, ForceMode.Impulse);
        StartCoroutine(COStartAbilityEffectTimer());
    }

    private IEnumerator COStartAbilityEffectTimer()
    {
        yield return new WaitForSeconds(abilityDuration);
        _abilityTarget.CanReadInput = true;
        CanReadInput = true;
    }

    private void FindTarget()
    {
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            if (!this.Equals(p))
                _abilityTarget = p;
        }
    }
}

