using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトプール
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    #region private
    /// <summary>ゲームオブジェクトの辞書</summary>
    private Dictionary<string, Queue<GameObject>> _pooledGameObjects = new Dictionary<string, Queue<GameObject>>();
    #endregion

    #region unity methods
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region public method
    /// <summary>
    /// オブジェクトの取得（なければ生成）
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public GameObject GetGameObject(GameObject prefab, Vector2 pos)
    {
        //プレハブの名前をkeyに
        string key = prefab.name;

        //Keyが存在しなければ作成
        if(_pooledGameObjects.ContainsKey(key) == false)
        {
            _pooledGameObjects.Add(key, new Queue<GameObject>());
        }

        Queue<GameObject> gameObjects = _pooledGameObjects[key];
        GameObject go = null;

        if(gameObjects.Count > 0)
        {
            //Queueから一つ取り出す
            go = gameObjects.Dequeue();
            //位置を設定
            go.transform.position = pos;
            //アクティブに
            go.SetActive(true);
        }
        else
        {
            //新たに作成
            go = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
            //clone防止
            go.name = prefab.name;
            //子に設定
            go.transform.parent = transform;
        }
        return go;
    }

    /// <summary>
    /// 非アクティブ化
    /// </summary>
    /// <param name="go"></param>
    public void ReleaseGameObject(GameObject go)
    {
        //非アクティブにする
        go.SetActive(false);

        string key = go.name;
        Queue<GameObject> gameObjects = _pooledGameObjects[key];
        gameObjects.Enqueue(go);
    }
    #endregion
}