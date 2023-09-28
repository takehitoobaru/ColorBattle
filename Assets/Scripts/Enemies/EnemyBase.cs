using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Enemyのベースクラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour, IDamagable
{
    #region property
    public EnemyType EnemyType => _enemyData.EnemyType;
    public int HP => _currentHP;
    public int MaxHP => _enemyData.HP;
    public int AttackAmount => _currentAttackAmount;
    public string EnemyColor => _enemyColor;
    public string AttackColor => _attackColor;
    #endregion

    #region serialize
    [Tooltip("敵のデータ")]
    [SerializeField]
    private EnemyData _enemyData = default;

    [Tooltip("敵のHPのテキスト")]
    [SerializeField]
    private TextMeshProUGUI _enemyHPText = default;

    [Tooltip("敵の色のImage")]
    [SerializeField]
    private Image _enemyColorImage = default;

    [Tooltip("敵の攻撃色のImage")]
    [SerializeField]
    private Image _enemyAttackColorImage = default;

    [Tooltip("ダメージエフェクト")]
    [SerializeField]
    private GameObject _damageEffect = default;

    [Tooltip("回復エフェクト")]
    [SerializeField]
    private GameObject _healEffect = default;

    [Tooltip("撃破エフェクト")]
    [SerializeField]
    private GameObject _breakEffect = default;
    #endregion

    #region protected
    /// <summary>現在のHP</summary>
    protected int _currentHP;
    /// <summary>現在の攻撃力</summary>
    protected int _currentAttackAmount;
    /// <summary>アクション番号</summary>
    protected int _actionCount = 0;
    /// <summary>敵の色</summary>
    protected string _enemyColor = "green";
    /// <summary>敵の攻撃の色</summary>
    protected string _attackColor = "green";
    #endregion

    #region private
    /// <summary>現在の最大体力</summary>
    private int _currentMaxHP;
    #endregion

    #region Event
    #endregion

    #region unity methods
    protected virtual void Awake()
    {
        _currentMaxHP = _enemyData.HP;
        _currentHP = _currentMaxHP;
        _currentAttackAmount = _enemyData.AttackAmount;
        SetEnemyColor();
    }

    protected virtual void Start()
    {
        _enemyHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnEnable()
    {
        _currentMaxHP = _enemyData.HP;
        _currentHP = _currentMaxHP;
        _currentAttackAmount = _enemyData.AttackAmount;
        _enemyHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();
        SetEnemyColor();
    }

    protected virtual void OnDisable()
    {

    }
    #endregion

    #region public method
    /// <summary>
    /// 被ダメ処理
    /// </summary>
    /// <param name="amount">ダメージ量</param>
    public void Damage(int amount)
    {
        GameManager.Instance.Log.UpdateLog(amount + "ダメージ!");
        ObjectPool.Instance.GetGameObject(_damageEffect, transform.position);

        _currentHP -= amount;
        _enemyHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();

        if (_currentHP <= 0)
        {
            ObjectPool.Instance.GetGameObject(_breakEffect, transform.position);
            GameManager.Instance.PlaySE(4);
            _currentHP = 0;
        }
    }

    /// <summary>
    /// 攻撃カラーのセット
    /// </summary>
    /// <param name="fieldColor"></param>
    public void SetAttackColor(string fieldColor)
    {
        switch (_enemyColor)
        {
            case "red":
                if (fieldColor == "red")
                {
                    _attackColor = "red";
                    _enemyAttackColorImage.color = Color.red;
                }
                else if (fieldColor == "green")
                {
                    _attackColor = "yellow";
                    _enemyAttackColorImage.color = Color.yellow;
                }
                else if (fieldColor == "blue")
                {
                    _attackColor = "magenta";
                    _enemyAttackColorImage.color = Color.magenta;
                }
                break;
            case "green":
                if (fieldColor == "red")
                {
                    _attackColor = "yellow";
                    _enemyAttackColorImage.color = Color.yellow;
                }
                else if (fieldColor == "green")
                {
                    _attackColor = "green";
                    _enemyAttackColorImage.color = Color.green;
                }
                else if (fieldColor == "blue")
                {
                    _attackColor = "cyan";
                    _enemyAttackColorImage.color = Color.cyan;
                }
                break;
            case "blue":
                if (fieldColor == "red")
                {
                    _attackColor = "magenta";
                    _enemyAttackColorImage.color = Color.magenta;
                }
                else if (fieldColor == "green")
                {
                    _attackColor = "cyan";
                    _enemyAttackColorImage.color = Color.cyan;
                }
                else if (fieldColor == "blue")
                {
                    _attackColor = "blue";
                    _enemyAttackColorImage.color = Color.blue;
                }
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 各敵のターンアクション
    /// </summary>
    public virtual void TurnAction(PlayerController player, IDamagable target)
    {
    }
    #endregion

    #region private method
    /// <summary>
    /// 敵の色を決める
    /// </summary>
    private void SetEnemyColor()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                _enemyColor = "red";
                _enemyColorImage.color = Color.red;
                break;
            case 1:
                _enemyColor = "green";
                _enemyColorImage.color = Color.green;
                break;
            case 2:
                _enemyColor = "blue";
                _enemyColorImage.color = Color.blue;
                break;
            default:
                break;
        }
    }
    #endregion

    #region protected method
    /// <summary>
    /// プレイヤーのカラーを確認する
    /// </summary>
    /// <param name="player">プレイヤー</param>
    protected void CheckPlayerColor(PlayerController player)
    {
        switch (_attackColor)
        {
            case "red":
                if (player.PlayerColor == "green")
                {
                    player.ChangeIsWeak(true);
                }
                else
                {
                    player.ChangeIsWeak(false);
                }
                break;
            case "green":
                if (player.PlayerColor == "blue")
                {
                    player.ChangeIsWeak(true);
                }
                else
                {
                    player.ChangeIsWeak(false);
                }
                break;
            case "blue":
                if (player.PlayerColor == "red")
                {
                    player.ChangeIsWeak(true);
                }
                else
                {
                    player.ChangeIsWeak(false);
                }
                break;
            case "magenta":
                if (player.PlayerColor == "green")
                {
                    player.ChangeIsWeak(true);
                }
                else
                {
                    player.ChangeIsWeak(false);
                }
                break;
            case "yellow":
                if (player.PlayerColor == "blue")
                {
                    player.ChangeIsWeak(true);
                }
                else
                {
                    player.ChangeIsWeak(false);
                }
                break;
            case "cyan":
                if (player.PlayerColor == "red")
                {
                    player.ChangeIsWeak(true);
                }
                else
                {
                    player.ChangeIsWeak(false);
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="player">プレイヤー</param>
    /// <param name="target">ターゲット</param>
    protected void Attack(PlayerController player, IDamagable target)
    {
        GameManager.Instance.Log.UpdateLog("敵の攻撃");
        GameManager.Instance.PlaySE(2);

        //プレイヤーの色確認
        CheckPlayerColor(player);

        int damage = _currentAttackAmount;
        //弱点ならダメージ２倍
        if (player.IsWeak == true)
        {
            GameManager.Instance.Log.UpdateLog("Weak!");
            damage *= 2;
        }
        target.Damage(damage);
    }

    /// <summary>
    /// 回復
    /// </summary>
    /// <param name="healAmount">回復量</param>
    protected void Heal(int healAmount)
    {
        GameManager.Instance.Log.UpdateLog("敵の回復:" + healAmount);
        ObjectPool.Instance.GetGameObject(_healEffect, transform.position);
        GameManager.Instance.PlaySE(3);

        _currentHP += healAmount;

        //最大体力超えたら
        if (_currentHP > _currentMaxHP)
        {
            _currentHP = _currentMaxHP;
        }

        _enemyHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();
    }

    protected void Wait()
    {
        GameManager.Instance.Log.UpdateLog("敵は様子を見ている");
    }
    #endregion
}