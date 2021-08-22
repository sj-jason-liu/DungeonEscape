using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel, _playerGemCount;

    private Player _player;

    private int _currentSelectedItem;
    private int _currentItemCost = 1000;
    private int _currentGemCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.CurrentDiamondCount());
                AudioManager.Instance.OpenShopSound();
                _currentGemCount = _player.CurrentDiamondCount();
            }
            if(_shopPanel != null)
            {
                _shopPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(_shopPanel != null)
            {
                _shopPanel.SetActive(false);
            }
        }
    }

    public void SelectItem(int item)
    {
        _currentSelectedItem = item;
        Debug.Log("Selected item: " + item);
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelect(85);
                _currentItemCost = 300;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelect(-39);
                _currentItemCost = 500;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelect(-163);
                _currentItemCost = 100;
                break;
        }
    }

    public void BuyingItem()
    {
        if(_player.CurrentDiamondCount() >= _currentItemCost)
        {
            _player.PurchasedDiamondCount(_currentItemCost);
            switch(_currentSelectedItem)
            {
                case 0:
                    GameManager.Instance.HasFlameSword = true;
                    AudioManager.Instance.GetSwordSound();
                    break;
                case 1:
                    GameManager.Instance.HasFlightBoots = true;
                    break;
                case 2:
                    GameManager.Instance.HasCastleKey = true;
                    break;
            }
            UIManager.Instance.UpdateHoldedItem(_currentSelectedItem);
        }
        else
        {
            //error message or animation
            _playerGemCount.GetComponent<Animation>().Play();
        }
    }
}
