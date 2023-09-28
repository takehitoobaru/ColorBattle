using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region property
    public int FloorNum => _floorNum;
    public int CurrentCost => _currentCost;
    public bool IsFloorPanel => _isFloorPanel;
    public PlayerController Player => _player;
    public SkillBase SelectedSkill => _selectedSkill;
    public LogSystem Log => _log;
    #endregion

    #region serialize
    [Tooltip("ログ")]
    [SerializeField]
    private LogSystem _log = default;

    [Tooltip("BGM")]
    [SerializeField]
    private AudioClip[] BGMs = default;

    [Tooltip("SE")]
    [SerializeField]
    private AudioClip[] SEs = default;

    [Tooltip("SE用オーディオソース")]
    [SerializeField]
    private AudioSource _audioSourceSE;

    [Tooltip("BGM用オーディオソース")]
    [SerializeField]
    private AudioSource _audioSourceBGM;
    #endregion

    #region private
    /// <summary>階層番号</summary>
    private int _floorNum = 1;
    /// <summary>現在のコスト</summary>
    private int _currentCost = 0;
    /// <summary>フロアパネル展開中かどうか</summary>
    private bool _isFloorPanel = true;
    /// <summary>プレイヤーのターンかどうか</summary>
    private bool _isPlayerTurn = true;
    /// <summary>選択されたスキル</summary>
    private SkillBase _selectedSkill;
    /// <summary>プレイヤー</summary>
    private PlayerController _player;
    #endregion

    #region Action
    /// <summary>階層変化時用</summary>
    public Action<int> OnFloorUpdate;
    /// <summary>コスト変化時用</summary>
    public Action<int> OnCostUpdate;
    #endregion

    #region unity methods
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    /// <summary>
    /// スキルを選択する
    /// </summary>
    /// <param name="skill">選択されるスキル</param>
    public void SetSkill(SkillBase skill)
    {
        if (_isPlayerTurn == false) return;
        _selectedSkill = skill;
    }

    /// <summary>
    /// ターンを変える
    /// </summary>
    public void ChangeTurn()
    {
        if (_isPlayerTurn == true) _isPlayerTurn = false;
        else _isPlayerTurn = true;
    }

    /// <summary>
    /// 選択されたスキルをnullにする
    /// </summary>
    public void ClearSelectedSkill()
    {
        _selectedSkill = null;
    }

    /// <summary>
    /// バトルフラグを切り替える
    /// </summary>
    public void ChangeFloorPanel()
    {
        if(_isFloorPanel == true)
        {
            _isFloorPanel = false;
        }
        else
        {
            _isFloorPanel = true;
        }
    }

    /// <summary>
    /// 階層を進める
    /// </summary>
    public void AddFloorNum()
    {
        _floorNum++;

        if(OnFloorUpdate != null)
        {
            OnFloorUpdate.Invoke(_floorNum);
        }
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="bgmNum">BGMの番号</param>
    public void PlayBGM(int bgmNum)
    {
        _audioSourceBGM.clip = BGMs[bgmNum];
        _audioSourceBGM.Play();
    }

    /// <summary>
    /// BGM停止
    /// </summary>
    public void StopBGM()
    {
        _audioSourceBGM.Stop();
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="seNum">SEの番号</param>
    public void PlaySE(int seNum)
    {
        _audioSourceSE.PlayOneShot(SEs[seNum]);
    }

    /// <summary>
    /// コストをアップ
    /// </summary>
    public void AddCost()
    {
        _currentCost++;

        if(OnCostUpdate != null)
        {
            OnCostUpdate.Invoke(_currentCost);
        }
    }

    /// <summary>
    /// コストを支払う
    /// </summary>
    /// <param name="cost">スキルコスト</param>
    public void PayCost(int cost)
    {
        _currentCost -= cost;

        if(OnCostUpdate != null)
        {
            OnCostUpdate.Invoke(_currentCost);
        }
    }

    /// <summary>
    /// コストをリセット
    /// </summary>
    public void ResetCost()
    {
        _currentCost = 0;

        if (OnCostUpdate != null)
        {
            OnCostUpdate.Invoke(_currentCost);
        }
    }
    #endregion

    #region private method
    #endregion
}