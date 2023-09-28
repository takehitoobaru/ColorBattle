using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Tooltip("リザルトをまとめたパネル")]
    [SerializeField]
    private CanvasGroup _panel = default;

    [Tooltip("フロアパネル")]
    [SerializeField]
    private CanvasGroup _floorPanel = default;

    [Tooltip("Healボタン")]
    [SerializeField]
    private Button _healButton = default;

    [Tooltip("色変更ボタン赤")]
    [SerializeField]
    private Button _redButton = default;

    [Tooltip("色変更ボタン緑")]
    [SerializeField]
    private Button _greenButton = default;
    
    [Tooltip("色変更ボタン青")]
    [SerializeField]
    private Button _blueButton = default;
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _healButton.onClick.AddListener(OnClickHeal);
        _redButton.onClick.AddListener(OnClickRed);
        _greenButton.onClick.AddListener(OnClickGreen);
        _blueButton.onClick.AddListener(OnClickBlue);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
    #endregion

    #region public method
    /// <summary>
    /// Healボタン押下時
    /// </summary>
    public void OnClickHeal()
    {
        GameManager.Instance.PlaySE(0);
        int healAmount = 10;
        GameManager.Instance.Player.Heal(healAmount);
        _panel.gameObject.SetActive(false);
        GameManager.Instance.AddFloorNum();
        _floorPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 赤ボタン押下時
    /// </summary>
    public void OnClickRed()
    {
        GameManager.Instance.PlaySE(0);
        GameManager.Instance.Player.ChangePlayerColor("red");
        _panel.gameObject.SetActive(false);
        GameManager.Instance.AddFloorNum();
        _floorPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 緑ボタン押下時
    /// </summary>
    public void OnClickGreen()
    {
        GameManager.Instance.PlaySE(0);
        GameManager.Instance.Player.ChangePlayerColor("green");
        _panel.gameObject.SetActive(false);
        GameManager.Instance.AddFloorNum();
        _floorPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 青ボタン押下時
    /// </summary>
    public void OnClickBlue()
    {
        GameManager.Instance.PlaySE(0);
        GameManager.Instance.Player.ChangePlayerColor("blue");
        _panel.gameObject.SetActive(false);
        GameManager.Instance.AddFloorNum();
        _floorPanel.gameObject.SetActive(true);
    }

    #endregion

    #region private method
    #endregion
}