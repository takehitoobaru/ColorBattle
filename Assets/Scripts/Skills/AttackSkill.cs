using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃スキル
/// </summary>
public class AttackSkill : SkillBase
{
    #region public method
    /// <summary>
    /// スキルの使用 単純な攻撃
    /// </summary>
    /// <param name="target">ターゲット</param>
    /// <param name="player">プレイヤー</param>
    public override void Use(EnemyBase enemy, PlayerController player)
    {
        GameManager.Instance.Log.UpdateLog("Slashスキル使用");
        //攻撃SE
        GameManager.Instance.PlaySE(1);
        //弱点ついたら2倍ダメージ
        if (player.IsAdvantage == true)
        {
            GameManager.Instance.Log.UpdateLog("Weak!");
            int damage = _attackAmount * 2;
            enemy.Damage(damage);
        }
        else
        {
            enemy.Damage(_attackAmount);
        }
    }
    #endregion
}