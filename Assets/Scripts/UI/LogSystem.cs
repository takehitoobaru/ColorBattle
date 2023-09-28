using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ログ
/// </summary>
public class LogSystem : MonoBehaviour
{
    #region serialize
    [Tooltip("ログテキスト")]
    [SerializeField]
    private TextMeshProUGUI _logText = default;
    #endregion

    #region private
    private string _logMessage;
    #endregion

    #region public method
    /// <summary>
    /// ログの更新
    /// </summary>
    /// <param name="message">追加したい文章</param>
    public void UpdateLog(string message)
    {
        _logMessage += message + "\n";
        _logText.text = _logMessage;
    }

    /// <summary>
    /// ログの初期化
    /// </summary>
    public void LogClear()
    {
        _logMessage = "";
        _logText.text = "";
    }
    #endregion
}