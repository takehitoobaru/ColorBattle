using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// バトルの処理
/// </summary>
public class Battle : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Tooltip("敵のプレハブ")]
    [SerializeField]
    private GameObject[] _enemyPrefabs = default;

    [Tooltip("ボスのプレハブ")]
    [SerializeField]
    private GameObject _BossPrefab = default;

    [Tooltip("フィールドカラーのテキスト")]
    [SerializeField]
    private Image _fieldColorImage = default;

    [Tooltip("コストのテキスト")]
    [SerializeField]
    private TextMeshProUGUI _costText = default;

    [Tooltip("ゲームエンドのテキスト")]
    [SerializeField]
    private TextMeshProUGUI _gameEndText = default;

    [Tooltip("到達階層のテキスト")]
    [SerializeField]
    private TextMeshProUGUI _GameEndFloorText = default;

    [Tooltip("floorpanelのボタン")]
    [SerializeField]
    private Button _floorPanelButton = default;

    [Tooltip("floorpanelのボタン")]
    [SerializeField]
    private Button _gameEndButton = default;

    [Tooltip("FloorPanel")]
    [SerializeField]
    private CanvasGroup _floorPanel = default;

    [Tooltip("ResultPanel")]
    [SerializeField]
    private CanvasGroup _resultPanel = default;

    [Tooltip("GameEndPanel")]
    [SerializeField]
    private CanvasGroup _gameEndPanel = default;

    [Tooltip("敵設置用のTransform")]
    [SerializeField]
    private Transform _enemyTransform = default;
    #endregion

    #region private
    /// <summary>場の色</summary>
    private string _fieldColor = "red";
    /// <summary>バトル用のフラグ</summary>
    private bool _battleInit = false;
    /// <summary>敵のリスト</summary>
    private List<EnemyBase> _activeEnemies = new List<EnemyBase>();
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _floorPanelButton.onClick.AddListener(OnClickFloorPanelButton);
        _gameEndButton.onClick.AddListener(OnClickGameEndButton);
    }

    private void Start()
    {
        GameManager.Instance.OnCostUpdate += UpdateCostUI;
    }

    private void Update()
    {
        if(GameManager.Instance.IsFloorPanel == false && _battleInit == false)
        {
            SetEnemies();
            GameManager.Instance.Log.UpdateLog("バトルスタート!");
            StartCoroutine(TurnBattle());
            GameManager.Instance.PlayBGM(0);
            _battleInit = true;
        }
    }
    #endregion

    #region public method
    /// <summary>
    /// フロアパネルボタン押下時の処理
    /// </summary>
    public void OnClickFloorPanelButton()
    {
        GameManager.Instance.ChangeFloorPanel();
        _floorPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// ゲームエンドボタン押下時の処理
    /// </summary>
    public void OnClickGameEndButton()
    {
        SceneManager.LoadSceneAsync("TitleScene");
    }
    #endregion

    #region private method
    /// <summary>
    /// 敵の設置
    /// </summary>
    private void SetEnemies()
    {
        //10階なら
        if (GameManager.Instance.FloorNum == 10)
        {
            var boss = ObjectPool.Instance.GetGameObject(_BossPrefab, _enemyTransform.position);
            _activeEnemies.Add(boss.GetComponent<EnemyBase>());
        }
        //それ以外
        else
        {
            int random = Random.Range(0, _enemyPrefabs.Length);
            var enemy = ObjectPool.Instance.GetGameObject(_enemyPrefabs[random], _enemyTransform.position);
            _activeEnemies.Add(enemy.GetComponent<EnemyBase>());
        }
    }

    /// <summary>
    /// 場の色を変える
    /// </summary>
    private void ChangeFieldColor()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                _fieldColor = "red";
                _fieldColorImage.color = Color.red;
                break;
            case 1:
                _fieldColor = "green";
                _fieldColorImage.color = Color.green;
                break;
            case 2:
                _fieldColor = "blue";
                _fieldColorImage.color = Color.blue;
                break;
        }
        GameManager.Instance.Log.UpdateLog("フィールドカラー変化");
    }

    /// <summary>
    /// コスト変化時の処理
    /// </summary>
    /// <param name="cost">コスト</param>
    private void UpdateCostUI(int cost)
    {
        _costText.text = cost.ToString();
    }

    /// <summary>
    /// ゲーム終了時の処理
    /// </summary>
    /// <param name="gameEndText"></param>
    private void GameEnd(string gameEndText)
    {
        _gameEndPanel.gameObject.SetActive(true);
        _gameEndText.text = gameEndText;
        _GameEndFloorText.text = "Floor " + GameManager.Instance.FloorNum.ToString() + "/10";
    }
    #endregion

    #region coroutine method
    /// <summary>
    /// ターンバトルの処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator TurnBattle()
    {
        while (true)
        {
            //プレイヤーの攻撃色をリセット
            GameManager.Instance.Player.ClearAttackColor();
            //場の色変更
            ChangeFieldColor();
            GameManager.Instance.Log.UpdateLog("プレイヤーのターン");
            //コストアップ
            GameManager.Instance.AddCost();
            _activeEnemies[0].SetAttackColor(_fieldColor);
            //スキルを選ぶまで待つ
            while (GameManager.Instance.SelectedSkill == null) yield return null;
            //攻撃カラーの決定
            GameManager.Instance.Player.SetAttackColor(_fieldColor);
            //相性判定
            GameManager.Instance.Player.CheckEnemyColor(_activeEnemies[0]);
            //スキルの使用
            GameManager.Instance.SelectedSkill.Use(_activeEnemies[0], GameManager.Instance.Player);
            //選択スキルをリセット
            GameManager.Instance.ClearSelectedSkill();
            //敵が死んだらループを抜ける
            if (_activeEnemies[0].HP <= 0)
            {
                //非アクティブに
                ObjectPool.Instance.ReleaseGameObject(_activeEnemies[0].gameObject);
                //listから削除
                _activeEnemies.Remove(_activeEnemies[0]);
                GameManager.Instance.Log.UpdateLog("勝利!");
                //1秒待つ
                yield return new WaitForSeconds(1);
                break;
            }
            GameManager.Instance.Log.UpdateLog("敵のターン");
            //敵ターンへ
            GameManager.Instance.ChangeTurn();
            //1秒待つ
            yield return new WaitForSeconds(1);
            //ターンアクションを行う
            _activeEnemies[0].TurnAction(GameManager.Instance.Player, GameManager.Instance.Player);
            //ターンを変える
            GameManager.Instance.ChangeTurn();

            //プレイヤーが死んだらループを抜ける
            if (GameManager.Instance.Player.HP <= 0)
            {
                GameManager.Instance.Log.UpdateLog("敗北");
                //1秒待つ
                yield return new WaitForSeconds(1);
                break;
            }
        }

        //選択スキルをリセット
        GameManager.Instance.ClearSelectedSkill();
        //コストをリセット
        GameManager.Instance.ResetCost();

        //敗北
        if (GameManager.Instance.Player.HP <= 0)
        {
            GameManager.Instance.PlayBGM(2);
            GameEnd("GameOver");
        }
        //ゲームクリア
        else if(GameManager.Instance.FloorNum >= 10)
        {
            GameManager.Instance.PlayBGM(1);
            GameEnd("GameClear");
        }
        //勝利
        else 
        {
            GameManager.Instance.StopBGM();
            _resultPanel.gameObject.SetActive(true);
            GameManager.Instance.ChangeFloorPanel();
            GameManager.Instance.Log.LogClear();
            _battleInit = false;

        }
    }
    #endregion
}