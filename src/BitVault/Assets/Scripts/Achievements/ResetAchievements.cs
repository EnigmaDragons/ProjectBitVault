#if UNITY_ANDROID || UNITY_IOS || UNITY_TIZEN || UNITY_TVOS || UNITY_WEBGL || UNITY_WSA || UNITY_PS4 || UNITY_WII || UNITY_XBOXONE || UNITY_SWITCH
#define DISABLESTEAMWORKS
#endif

using UnityEngine;

#if !DISABLESTEAMWORKS
using Steamworks;
#endif

public class ResetAchievements : MonoBehaviour
{
    [SerializeField] private BoolVariable _developmentToolsActive;

#if !DISABLESTEAMWORKS
    private void Update()
    {
        if (_developmentToolsActive.Value && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.H) && SteamManager.Initialized)
            SteamUserStats.ResetAllStats(true);
    }
#endif
}
