using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Text _playerGemCount;
    [SerializeField]
    private Image _selectImg;
    [SerializeField]
    private Text _gemCountText;

    public void OpenShop(int gemCount)
    {
        //update player gem count equal to Player gems
        _playerGemCount.text = gemCount + "G";
    }

    public void UpdateShopSelect(int yAxis)
    {
        _selectImg.rectTransform.anchoredPosition
            = new Vector2(_selectImg.rectTransform.anchoredPosition.x, yAxis);
    }

    public void UpdateGemCount(int count)
    {
        _gemCountText.text = "" + count;
    }
}
