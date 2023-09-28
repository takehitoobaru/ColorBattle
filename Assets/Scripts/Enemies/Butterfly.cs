using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 蝶の敵
/// </summary>
public class Butterfly : EnemyBase
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
            //攻撃
            case 0:
                Attack(player, target);
                _actionCount++;
                break;
            //回復
            case 1:
                Heal(2);
                _actionCount = 0;
                break;
            default:
                Debug.LogError("アクションエラー");
                break;
        }
    }
    #endregion

    #region private method
    #endregion
}