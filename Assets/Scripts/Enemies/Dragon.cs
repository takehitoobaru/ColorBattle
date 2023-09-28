using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボス
/// </summary>
public class Dragon : EnemyBase
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
        int rand = Random.Range(0, 100);

        if(rand < 70)
        {
            Attack(player, target);
        }
        else if(rand < 90)
        {
            Heal(5);
        }
        else
        {
            GameManager.Instance.Log.UpdateLog("強攻撃!");
            //攻撃力倍
            _currentAttackAmount *= 2;
            Attack(player, target);
            //元に戻す
            _currentAttackAmount = _currentAttackAmount / 2;
        }
    }
    #endregion

    #region private method
    #endregion
}