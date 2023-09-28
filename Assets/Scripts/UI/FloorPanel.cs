using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorPanel : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Tooltip("階層テキスト")]
    [SerializeField]
    private TextMeshProUGUI _floorText = default;
    #endregion

    #region unity methods

    private void Start()
    {
        GameManager.Instance.OnFloorUpdate += UpdateFloorUI;
    }
    #endregion

    #region private method
    private void UpdateFloorUI(int floorNum)
    {
        _floorText.text = "Floor" + floorNum.ToString();
    }
    #endregion
}