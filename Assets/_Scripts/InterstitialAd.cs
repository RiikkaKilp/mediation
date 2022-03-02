using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

public class InterstitialAd : MonoBehaviour
{
    IInterstitialAd ad;
    string adUnitId = "Interstitial_Android";
    string gameId = "4638146";

    public AdLog adLog;

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

    #endregion

    #region LoadAd
    public void SetupAd()
    {
        //Create
        ad = MediationService.Instance.CreateInterstitialAd(adUnitId);

        //Subscribe to events
        ad.OnLoaded += AdLoaded;
        ad.OnFailedLoad += AdFailedLoad;

        ad.OnShowed += AdShown;
        ad.OnFailedShow += AdFailedShow;
        ad.OnClosed += AdClosed;
        ad.OnClicked += AdClicked;

        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
    }

    void AdLoaded(object sender, EventArgs args)
    {
        adLog.UpdateLog("Interstitial Ad loaded");
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
        //debug
        if (ad.AdState == AdState.Loaded)
        {
            ad.Show();
            adLog.UpdateLog("Show Interstitial Ad");
        }
        else
            adLog.UpdateLog("Interstitial Ad not loaded!");
    }

    void AdShown(object sender, EventArgs args)
    {

    }

    void AdClosed(object sender, EventArgs e)
    {
        // Pre-load the next ad
        ad.Load();
        // Execute logic after an ad has been closed.
    }
    void AdFailedShow(object sender, ShowErrorEventArgs args)
    {
        Debug.Log(args.Message);
    }
    #endregion

    #region AdInteractions
    void AdClicked(object sender, EventArgs e)
    {
        Debug.Log("Ad has been clicked");
        // Execute logic after an ad has been clicked.
    }

    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        //Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
    }
    #endregion
}
