using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エフェクトの処理
/// </summary>
public class Effect : MonoBehaviour
{
    #region public method
    public void EndAnimation()
    {
        ObjectPool.Instance.ReleaseGameObject(gameObject);
    }
    #endregion
}