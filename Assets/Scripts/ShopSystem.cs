using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel;

    private Player _player;

    private int _currentSelectedItem;
    private int _currentItemCost;
    private int _currentGemCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.CurrentDiamondCount());
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
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelect(90);
                _currentItemCost = 300;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelect(-13);
                _currentItemCost = 500;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelect(-125);
                _currentItemCost = 100;
                break;
        }
    }

    public void BuyingItem()
    {
        if(_player.CurrentDiamondCount() >= _currentItemCost)
        {
            //get item
            _player.PurchasedDiamondCount(_currentItemCost);
        }
        else
        {
            //error message or animation
        }
    }
}
