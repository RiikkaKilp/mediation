using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdLog : MonoBehaviour
{
    public TextMeshProUGUI debugLog;
    public TextMeshProUGUI rewardLog;

    // Suggestions:
    // Clear the log once the string gets too large, or just write a different system for logs alltogether
    private void Start()
    {
        debugLog.text = "";
        rewardLog.text = "No rewards";
    }
    public void UpdateLog(string updateText)
    {
        debugLog.text += "\n" + updateText;
    }

    public void UpdateRewardLog(string updateText)
    {
        rewardLog.text = updateText;
    }
}
