using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

public class RewardedAd : MonoBehaviour
{
    IRewardedAd ad;
    string adUnitId = "Rewarded_Android";
    string gameId = "4638146";

    public AdLog adLog;
    private int rewardCount = 0;
    private int shownAdCount = 0;

    #region Custom

    public void LoadAdvertisement()
    {
        SetupAd();
        ad.Load();
    }

    public void ShowAdvertisement()
    {
        ShowAd();
    }

    private void UpdateLog()
    {
        adLog.UpdateRewardLog("Shown video ads: " + shownAdCount.ToString() + "\nRewards: " + rewardCount.ToString());
    }
    #endregion

    #region LoadAd
    public void SetupAd()
    {
        //Create
        ad = MediationService.Instance.CreateRewardedAd(adUnitId);

        //Subscribe to events
        ad.OnLoaded += AdLoaded;
        ad.OnFailedLoad += AdFailedLoad;

        ad.OnShowed += AdShown;
        ad.OnFailedShow += AdFailedShow;
        ad.OnClosed += AdClosed;
        ad.OnClicked += AdClicked;
        ad.OnUserRewarded += UserRewarded;

        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
    }
    void AdLoaded(object sender, EventArgs args)
    {
        adLog.UpdateLog("Rewarded Ad loaded");
    }

    void AdFailedLoad(object sender, LoadErrorEventArgs args)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(args.Message);
    }
    #endregion

    #region ShowAd
    public void ShowAd()
    {
        if (ad.AdState == AdState.Loaded)
        {
            ad.Show();
            shownAdCount++;
            UpdateLog();
            adLog.UpdateLog("Show Rewarded Ad");
        }
        else
            adLog.UpdateLog("Rewarded Ad not loaded!");
    }

    void AdShown(object sender, EventArgs args)
    {

    }

    void AdFailedShow(object sender, ShowErrorEventArgs args)
    {
        Debug.Log(args.Message);
    }
    #endregion

    #region AdInteractions
    void AdClosed(object sender, EventArgs e)
    {
        // Pre-load the next ad
        ad.Load();
        // Execute logic after an ad has been closed.
    }

    void AdClicked(object sender, EventArgs e)
    {
        // Execute logic after an ad has been clicked.
    }
    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        //Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
    }

    void UserRewarded(object sender, RewardEventArgs e)
    {
        rewardCount++;
        UpdateLog();
    }
    #endregion
}
