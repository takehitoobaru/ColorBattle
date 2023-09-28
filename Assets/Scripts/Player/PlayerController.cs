using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// プレイヤーの機能全般
/// </summary>
public class PlayerController : MonoBehaviour, IDamagable
{
    #region property
    public int MaxHP => _currentMaxHP;
    public int HP => _currentHP;
    public string PlayerColor => _playerColor;
    public string AttackColor => _attackColor;
    public bool IsAdvantage => _isAdvantage;
    public bool IsWeak => _isWeak;
    #endregion

    #region serialize
    [Tooltip("ゲームスタート時のHP")]
    [SerializeField]
    private int _startHP = 50;

    [Tooltip("プレイヤーのHP表示用テキスト")]
    [SerializeField]
    private TextMeshProUGUI _playerHPText = default;

    [Tooltip("プレイヤーの色表示用Image")]
    [SerializeField]
    private Image _playerColorImage = default;

    [Tooltip("プレイヤーの攻撃色表示用Image")]
    [SerializeField]
    private Image _playerAttackColorImage = default;

    [Tooltip("ダメージエフェクト")]
    [SerializeField]
    private GameObject _playerDamageEffect = default;

    [Tooltip("回復エフェクト")]
    [SerializeField]
    private GameObject _playerHealEffect = default;
    #endregion

    #region private
    /// <summary>現在の最大HP</summary>
    private int _currentMaxHP;
    /// <summary>現在のHP</summary>
    private int _currentHP;
    /// <summary>プレイヤーの色</summary>
    private string _playerColor = "red";
    /// <summary>攻撃の色</summary>
    private string _attackColor = "red";
    /// <summary>弱点を突いているかどうか</summary>
    private bool _isAdvantage = false;
    /// <summary>弱点を突かれているかどうか</summary>
    private bool _isWeak = false;
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _currentMaxHP = _startHP;
        _currentHP = _currentMaxHP;
        ChangePlayerColor("red");
    }

    private void Start()
    {
        _playerHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();
        _playerAttackColorImage.color = Color.white;
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    /// <summary>
    /// 被ダメージ
    /// </summary>
    /// <param name="amount">ダメージ値</param>
    public void Damage(int amount)
    {
        //ログ表示
        GameManager.Instance.Log.UpdateLog(amount + "ダメージ!");
        //エフェクト表示
        ObjectPool.Instance.GetGameObject(_playerDamageEffect, transform.position);
        //HP減少
        _currentHP -= amount;
        _playerHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();

        //死亡時
        if (_currentHP <= 0)
        {
            GameManager.Instance.PlaySE(4);
            _currentHP = 0;
        }
    }

    /// <summary>
    /// 回復
    /// </summary>
    /// <param name="amount">回復量</param>
    public void Heal(int amount)
    {
        //ログ表示
        GameManager.Instance.Log.UpdateLog("プレイヤー回復:" + amount);
        //エフェクト表示
        ObjectPool.Instance.GetGameObject(_playerHealEffect, transform.position);
        //SE再生
        GameManager.Instance.PlaySE(3);

        //回復
        _currentHP += amount;

        //最大値を超えたら
        if (_currentHP > _currentMaxHP)
        {
            _currentHP = _currentMaxHP;
        }

        _playerHPText.text = "HP:" + _currentHP.ToString() + "/" + _currentMaxHP.ToString();
    }

    /// <summary>
    /// 最大HPアップ
    /// </summary>
    /// <param name="amount"></param>
    public void MaxHPUp(int amount)
    {
        _currentMaxHP += amount;
        _currentHP += amount;
    }

    /// <summary>
    /// 攻撃カラーを決める
    /// </summary>
    /// <param name="fieldColor">フィールドカラー</param>
    public void SetAttackColor(string fieldColor)
    {
        switch (_playerColor)
        {
            case "red":
                if(fieldColor == "red")
                {
                    _attackColor = "red";
                    _playerAttackColorImage.color = Color.red;
                }
                else if(fieldColor == "green")
                {
                    _attackColor = "yellow";
                    _playerAttackColorImage.color = Color.yellow;
                }
                else if(fieldColor == "blue")
                {
                    _attackColor = "magenta";
                    _playerAttackColorImage.color = Color.magenta;
                }
                break;
            case "green":
                if (fieldColor == "red")
                {
                    _attackColor = "yellow";
                    _playerAttackColorImage.color = Color.yellow;
                }
                else if (fieldColor == "green")
                {
                    _attackColor = "green";
                    _playerAttackColorImage.color = Color.green;
                }
                else if (fieldColor == "blue")
                {
                    _attackColor = "cyan";
                    _playerAttackColorImage.color = Color.cyan;
                }
                break;
            case "blue":
                if (fieldColor == "red")
                {
                    _attackColor = "magenta";
                    _playerAttackColorImage.color = Color.magenta;
                }
                else if (fieldColor == "green")
                {
                    _attackColor = "cyan";
                    _playerAttackColorImage.color = Color.cyan;
                }
                else if (fieldColor == "blue")
                {
                    _attackColor = "blue";
                    _playerAttackColorImage.color = Color.blue;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 攻撃色を初期化
    /// </summary>
    public void ClearAttackColor()
    {
        _attackColor = "";
        _playerAttackColorImage.color = Color.white;
    }

    /// <summary>
    /// 敵の属性との相性を調べる
    /// </summary>
    /// <param name="enemy">敵</param>
    public void CheckEnemyColor(EnemyBase enemy)
    {
        switch (_attackColor)
        {
            case "red":
                if(enemy.EnemyColor == "green")
                {
                    _isAdvantage = true;
                }
                else
                {
                    _isAdvantage = false;
                }
                break;
            case "green":
                if (enemy.EnemyColor == "blue")
                {
                    _isAdvantage = true;
                }
                else
                {
                    _isAdvantage = false;
                }
                break;
            case "blue":
                if (enemy.EnemyColor == "red")
                {
                    _isAdvantage = true;
                }
                else
                {
                    _isAdvantage = false;
                }
                break;
            case "magenta":
                if (enemy.EnemyColor == "green")
                {
                    _isAdvantage = true;
                }
                else
                {
                    _isAdvantage = false;
                }
                break;
            case "yellow":
                if (enemy.EnemyColor == "blue")
                {
                    _isAdvantage = true;
                }
                else
                {
                    _isAdvantage = false;
                }
                break;
            case "cyan":
                if (enemy.EnemyColor == "red")
                {
                    _isAdvantage = true;
                }
                else
                {
                    _isAdvantage = false;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// プレイヤーカラーの変更
    /// </summary>
    /// <param name="color">変更する色</param>
    public void ChangePlayerColor(string color)
    {
        _playerColor = color;
        if(color == "red")
        {
            _playerColorImage.color = Color.red;
        }
        else if(color == "green")
        {
            _playerColorImage.color = Color.green;
        }
        else if(color == "blue")
        {
            _playerColorImage.color = Color.blue;
        }
    }

    public void ChangeIsWeak(bool weak)
    {
        _isWeak = weak;
    }
    #endregion

    #region private method
    #endregion
}