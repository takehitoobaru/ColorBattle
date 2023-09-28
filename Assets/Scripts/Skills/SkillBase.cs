using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スキルのベース
/// </summary>
public abstract class SkillBase : MonoBehaviour
{

    #region property
    public int AttackAmount => _attackAmount;
    public int HealAmount => _healAmount;
    public int RequiredCost => _requiredCost;
    #endregion

    #region serialize
    [Tooltip("攻撃力")]
    [SerializeField]
    protected int _attackAmount = 0;

    [Tooltip("回復力")]
    [SerializeField]
    protected int _healAmount = 0;

    [Tooltip("必要コスト")]
    [SerializeField]
    protected int _requiredCost = 0;

    [Tooltip("ボタン")]
    [SerializeField]
    protected Button _skillbutton = default;
    #endregion

    #region unity methods
    private void Awake()
    {
        _skillbutton.onClick.AddListener(OnCliskSkillButton);
    }
    #endregion

    #region public method
    /// <summary>
    /// スキルの使用
    /// </summary>
    public abstract void Use(EnemyBase enemy,PlayerController player);

    public void OnCliskSkillButton()
    {
        //必要コストに満たない場合
        if (GameManager.Instance.CurrentCost < _requiredCost)
        {
            GameManager.Instance.PlaySE(5);
        }
        else
        {
            GameManager.Instance.PayCost(_requiredCost);
            GameManager.Instance.SetSkill(this);
        }
    }
    #endregion
}