using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    string gameId = "4638146";

    public async void InitServices()
    {
        try
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(gameId);
            await UnityServices.InitializeAsync(initializationOptions);
        }
        catch (Exception e)
        {
            InitializationFailed(e);
        }
    }

    void InitializationFailed(Exception e)
    {
        Debug.Log("Initialization Failed: " + e.Message);
    }
}
