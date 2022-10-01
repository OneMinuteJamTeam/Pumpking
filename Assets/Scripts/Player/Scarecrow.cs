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

    private void CheckAbilityRange()
    {
        if(Vector3.Distance(_abilityTarget.transform.position, this.transform.position) < abilityRange)
        {
            _targetInRange = true;
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Target in range at Position " + _abilityTarget.transform.position);
        }
        else
        {
            _targetInRange = false;
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Target Loss");
        }
    }
}
