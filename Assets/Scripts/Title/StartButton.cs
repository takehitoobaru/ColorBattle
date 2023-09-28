using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// スタートボタン
/// </summary>
public class StartButton:MonoBehaviour
{
    [SerializeField]
    private AudioClip _titleSE = default;

    private AudioSource _titleAudioSource;

    private void Awake()
    {
        _titleAudioSource = GetComponent<AudioSource>();
    }

    #region public method
    /// <summary>
    /// スタートボタン押下時の処理
    /// </summary>
    public void OnClickStart()
    {
        float delay = _titleSE.length;
        _titleAudioSource.PlayOneShot(_titleSE);
        StartCoroutine(WaitLoadSceneCoroutine(delay));
    }
    #endregion

    /// <summary>
    /// SE再生が終わってからシーン遷移
    /// </summary>
    /// <param name="time">SEの長さ</param>
    /// <returns></returns>
    private IEnumerator WaitLoadSceneCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync("InGameScene");
    }
}