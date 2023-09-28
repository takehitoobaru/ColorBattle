using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵データのScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "MyScriptable/EnemyData")]
public class EnemyData : ScriptableObject
{
    #region property
    public EnemyType EnemyType => _enemyType;
    public int HP => _hp;
    public int AttackAmount => _attackAmount;
    #endregion

    #region serialize
    [Tooltip("敵の種類")]
    [SerializeField]
    private EnemyType _enemyType = default;

    [Tooltip("敵のHP")]
    [SerializeField]
    private int _hp = 10;

    [Tooltip("敵の攻撃力")]
    [SerializeField]
    private int _attackAmount = 1;
    #endregion
}

/// <summary>
/// 敵の種類
/// </summary>
public enum EnemyType
{
    /// <summary>蝶</summary>
    Butterfly,
    /// <summary>グリフォン</summary>
    Griffon,
    /// <summary>リザードマン</summary>
    Lizardman,
    /// <summary>触手</summary>
    Tentacle,
    /// <summary>ドラゴン（ボス）</summary>
    Dragon
}