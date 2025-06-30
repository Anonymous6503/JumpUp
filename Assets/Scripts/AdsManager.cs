/*
using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    
    private string adMobAppId = "";
    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardBasedVideoAd _rewardBasedVideoAd;

    private string TestDeviceId = "";

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        //Initialize Ads
        InitializeAds();
    }

    void InitializeAds()
    {
        //MobileAds.Initialize(adMobAppId);
        MobileAds.Initialize(adMobAppId);
        _rewardBasedVideoAd = RewardBasedVideoAd.Instance;
        RequestBanner();
        RequestInterstitial();
        RequestRewardedVideo();
    }

    #region BannerAd
    
    void RequestBanner()
    {
        string bannerId = "";
        _bannerView = new BannerView(bannerId,AdSize.Banner, AdPosition.Bottom);

        _bannerView.OnAdLoaded += HandleBannerOnAdLoaded;
        _bannerView.OnAdFailedToLoad += HandleBannerOnAdFailedToLoad;
        _bannerView.OnAdOpening += HandleBannerOnAdOpened;
        _bannerView.OnAdClosed += HandleBannerOnAdClosed;
        _bannerView.OnAdLeavingApplication += HandleBannerOnAdLeavingApplication;
        
        //AdRequest request = new AdRequest.Builder().AddTestDevice(TestDeviceId).Build();
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }

    public void HideBanner()
    {
        _bannerView.Hide();
    }

    public void DestroyBanner()
    {
        _bannerView.Destroy();
    }

//    public void DestroyBanner()
//    {
//        _bannerView.Destroy();
//    }
    
    #endregion
    
    #region IntersitialAd
    void RequestInterstitial()
    {
      
        string interstitalId = "";
        _interstitialAd = new InterstitialAd(interstitalId);

        _interstitialAd.OnAdLoaded += HandleInterstitialOnAdLoaded;
        _interstitialAd.OnAdFailedToLoad += HandleInterstitialOnAdFailedToLoad;
        _interstitialAd.OnAdOpening += HandleInterstitialOnAdOpened;
        _interstitialAd.OnAdClosed += HandleInterstitialOnAdClosed;
        _interstitialAd.OnAdLeavingApplication += HandleInterstitialOnAdLeavingApplication;
        
        //AdRequest interstitialRequest = new AdRequest.Builder().AddTestDevice(TestDeviceId).Build();
        AdRequest interstitialRequest = new AdRequest.Builder().Build();

        _interstitialAd.LoadAd(interstitialRequest);
    }
    bool IsInterstitialAdAvailable()
    {
        if (_interstitialAd.IsLoaded())
            return true;
        return false;
    }

    public void ShowInterstitialAd()
    {
        if (IsInterstitialAdAvailable())
            _interstitialAd.Show();
        else
            RequestInterstitial();
    }
    
    #endregion

    #region RewardedVideoAd
    void RequestRewardedVideo()
    {
       
        string rewardId = "";
        _rewardBasedVideoAd.OnAdLoaded += HandleRewardedOnAdLoaded;
        _rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardedOnAdFailedToLoad;
        _rewardBasedVideoAd.OnAdOpening += HandleRewardedOnAdOpened;
        _rewardBasedVideoAd.OnAdStarted += HandleRewardedOnAdStarted;
        _rewardBasedVideoAd.OnAdRewarded += HandleRewardedOnAdRewarded;
        _rewardBasedVideoAd.OnAdClosed += HandleRewardedOnAdClosed;
        _rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardedOnAdLeavingApplication;
        
        //AdRequest request = new AdRequest.Builder().AddTestDevice(TestDeviceId).Build();
        AdRequest request = new AdRequest.Builder().Build();
        _rewardBasedVideoAd.LoadAd(request, rewardId);

    }

    bool IsVideoAdAvailable()
    {
        if (_rewardBasedVideoAd.IsLoaded())
            return true;
        return false;
    }

    public void ShowVideoRewardAd()
    {
        if (IsVideoAdAvailable())
            _rewardBasedVideoAd.Show();
        else
           RequestRewardedVideo();
    }
    #endregion

    
    #region EventsSubscribtionFuncs

    #region Banner Events
    public void HandleBannerOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
        ShowBanner();
    }

    public void HandleBannerOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: "
              + args.Message);
    }

    public void HandleBannerOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleBannerOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleBannerOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }
    #endregion
    #region Interstitial Events
    public void HandleInterstitialOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleInterstitialOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: "
              + args.Message);
    }

    public void HandleInterstitialOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleInterstitialOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
        RequestInterstitial();
    }

    public void HandleInterstitialOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }
    #endregion
    #region Rewarded Events

    public void HandleRewardedOnAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleRewardedOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: "
              + args.Message);
        RequestRewardedVideo();
        GameManager.instance.CancelContinueOption();
    }

    public void HandleRewardedOnAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleRewardedOnAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
        RequestRewardedVideo();
    }

    public void HandleRewardedOnAdLeavingApplication(object sender, EventArgs args)
    {
        print("HandleAdLeavingApplication event received");
    }
    public void HandleRewardedOnAdStarted(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardedOnAdRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardBasedVideoRewarded event received for "
            + amount.ToString() + " " + type);
        //Show That You've been rewarded
        //GameManager.instance.gameOverMenu.SetActive(false);
        GameManager.instance.HideGameOverMenu();
        PlayerInteraction.instance.SetPlayerDeadState(false);
        PlayerInteraction.instance.SetPlayerPosition();
    }
    #endregion    


    #endregion
}
*/
