using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static ADManager instance;

    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isPlayStore = true;
    [SerializeField] private bool isTestAd = true;

    private string gameId;
    private string interstitialAdUnitId = "Interstitial_Android";
    private string rewardedAdUnitId = "Rewarded_Android";
    private string bannerAdUnitId = "Banner_Android";

    private bool interstitialLoaded = false;
    private bool rewardedLoaded = false;
    private Action<UnityAdsShowCompletionState> _onAdComplete;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAds()
    {
        gameId = isPlayStore ? androidGameId : iosGameId;
        Advertisement.Initialize(gameId, isTestAd, this);
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(interstitialAdUnitId, this);
        Advertisement.Load(rewardedAdUnitId, this);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(bannerAdUnitId);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"[Ads] Initialization Failed: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == interstitialAdUnitId)
            interstitialLoaded = true;
        else if (adUnitId == rewardedAdUnitId)
            rewardedLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"[Ads] Failed to Load {adUnitId}: {error} - {message}");
    }

    public void ShowInterstitialAd(Action<UnityAdsShowCompletionState> callback)
    {
        if (interstitialLoaded)
        {
            _onAdComplete = callback;
            Advertisement.Show(interstitialAdUnitId, this);
        }
        else
        {
            Debug.Log("[Ads] Interstitial not loaded yet.");
        }
    }

    public void ShowRewardedAd(Action<UnityAdsShowCompletionState> callback)
    {
        if (rewardedLoaded)
        {
            _onAdComplete = callback;
            Advertisement.Show(rewardedAdUnitId, this);
        }
        else
        {
            Debug.Log("[Ads] Rewarded ad not loaded yet.");
        }
    }

    public void ShowBannerAd()
    {
        Advertisement.Banner.Show(bannerAdUnitId);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        _onAdComplete?.Invoke(showCompletionState);

        // Reload for next time
        if (adUnitId == interstitialAdUnitId)
        {
            interstitialLoaded = false;
            Advertisement.Load(interstitialAdUnitId, this);
        }
        else if (adUnitId == rewardedAdUnitId)
        {
            rewardedLoaded = false;
            Advertisement.Load(rewardedAdUnitId, this);
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"[Ads] Show Failed for {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
