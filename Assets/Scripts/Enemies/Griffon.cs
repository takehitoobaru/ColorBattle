using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 毎ターンアタックする敵
/// </summary>
public class Griffon : EnemyBase
{
    #region unity methods
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _actionCount = 0;
    }
    #endregion

    #region public method
    public override void TurnAction(PlayerController player, IDamagable target)
    {
        Attack(player, target);
    }
    #endregion

}