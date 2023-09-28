using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回復スキル
/// </summary>
public class HealSkill : SkillBase
{
    #region public method
    /// <summary>
    /// スキルの使用 回復
    /// </summary>
    /// <param name="target">ターゲット</param>
    /// <param name="player">プレイヤー</param>
    public override void Use(EnemyBase enemy, PlayerController player)
    {
        GameManager.Instance.Log.UpdateLog("Healスキル使用");

        player.Heal(_healAmount);
    }
    #endregion
}