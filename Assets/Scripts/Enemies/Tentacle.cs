using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3ターンに一度強力な攻撃
/// </summary>
public class Tentacle : EnemyBase
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
        switch (_actionCount)
        {
            case 2:
                Attack(player, target);
                _actionCount = 0;
                break;
            default:
                Wait();
                _actionCount++;
                break;
        }
    }
    #endregion
}