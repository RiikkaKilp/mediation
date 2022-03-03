using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    string gameId = "4638146";

    public AdLog adLog;

    public async void InitServices()
    {
        try
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(gameId);
            await UnityServices.InitializeAsync(initializationOptions);
            adLog.UpdateLog("Initialization complete");
        }
        catch (Exception e)
        {
            InitializationFailed(e);
        }
    }

    void InitializationFailed(Exception e)
    {
        adLog.UpdateLog("Initialization failed!");
    }
}
