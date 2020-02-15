using TMPro;
using UnityEngine;

public sealed class DisplayCampaignStatusString : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private CurrentZone zone;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI description;
    
    void Awake()
    {
        var status = new CampaignStatus(InstallId.FromPlayerPrefs(), zone.Campaign, storage.SaveData);
        var statusString = CampaignStatusString.CreateFrom(status);
        if (description != null)
            description.text = status.ToString();
        text.text = statusString.Value;
    }
}
