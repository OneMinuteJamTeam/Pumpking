using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class Scarecrow : Player
{
    public float AbilityRange { get => abilityRange; }
    public bool HasTarget { get=> _targetInRange; }
    public Player AbilityTarget { get=> _abilityTarget; }

    [Header("Scarecrow Settings")]
    [SerializeField]
    private float abilityRange = 3.0f;
    [SerializeField]
    private float pushForce = 5.0f;
    [SerializeField]
    private float pullForce = 5.0f;
    [SerializeField]
    private float abilityDuration = 2.0f;

    private Player _abilityTarget = null;
    private bool _targetInRange = false;

    protected override void Awake()
    {
        base.Awake();

        Player[] players = FindObjectsOfType<Player>();
        foreach (Player p in players)
        {
            if(!this.Equals(p))
                _abilityTarget = p;
        }
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
            canUseAblity = true;
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
        if(Vector3.Distance(_abilityTarget.transform.position, this.transform.position) < abilityRange)
        {
            _targetInRange = true;
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Target InRange");
        }
        else
        {
            _targetInRange = false;
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Target Loss");
        }
    }

    private void PushAbility()
    {
        Vector3 dir = (_abilityTarget.transform.position - transform.position).normalized;

        _abilityTarget.CanReadInput = false;
        _abilityTarget.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _abilityTarget.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
        StartCoroutine(COStartAbilityEffectTimer());
    }

    private void PullAbility()
    {
        Vector3 dir = (transform.position - _abilityTarget.transform.position).normalized;

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
}

