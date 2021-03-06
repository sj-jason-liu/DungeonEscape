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
    [SerializeField]
    private Image[] _lives;
    [SerializeField]
    private GameObject _gameOverScreen, _needKeyScreen, _gamePassScreen;
    [SerializeField]
    private Image[] _holdedItems;

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

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 4; i != livesRemaining; i--)
        {
            _lives[i-1].enabled = false;
            if (i < 0)
                i = 0;
        }
    }

    public void UpdateHoldedItem(int itemID)
    {
        _holdedItems[itemID].enabled = false;
        _holdedItems[itemID + 3].enabled = true;
    }

    public void NeedKeyText()
    {
        _needKeyScreen.SetActive(true);
        StartCoroutine(NeedKeyTextRoutine());
    }

    IEnumerator NeedKeyTextRoutine()
    {
        yield return new WaitForSeconds(3f);
        _needKeyScreen.SetActive(false);
    }

    public void GamePassScreen()
    {
        _gamePassScreen.SetActive(true);
    }

    public void GameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }
}
