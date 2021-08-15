using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField]
    private int _rewardGems = 100;
    [SerializeField]
    private string _adUnitId, _androidGameId;
    private Player _player;
    [SerializeField]
    private Button _watchAdButton;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is NULL!");
        _watchAdButton.interactable = Advertisement.IsReady(_adUnitId);
        Advertisement.AddListener(this);
        Advertisement.Initialize(_androidGameId, true);
    }

    public void PlayRewardAd()
    {
        Advertisement.Show(_adUnitId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == _adUnitId)
            _watchAdButton.interactable = true;
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ads function is error!");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Finished:
                _player.AddDiamond(_rewardGems);
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //nothing to do
    }
}
