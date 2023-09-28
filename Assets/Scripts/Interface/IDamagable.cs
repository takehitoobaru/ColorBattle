using UnityEngine;

/// <summary>
/// 被ダメージ機能のインターフェース
/// </summary>
public interface IDamagable
{
    /// <summary>
    /// 被ダメージ
    /// </summary>
    /// <param name="amount">ダメージ量</param>
    void Damage(int amount);
}