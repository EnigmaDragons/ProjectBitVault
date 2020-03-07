using Steamworks;
using UnityEngine;

public class ResetAchievements : MonoBehaviour
{
    [SerializeField] private BoolVariable _developmentToolsActive;

    private void Update()
    {
        if (_developmentToolsActive.Value && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.H) && SteamManager.Initialized)
            SteamUserStats.ResetAllStats(true);
    }
}
