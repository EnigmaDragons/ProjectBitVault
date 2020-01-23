using UnityEngine;

public class SyncBetaModeCampaign : MonoBehaviour
{
    [SerializeField] private CurrentZone zone;
    [SerializeField] private Campaign normalCampaign;
    [SerializeField] private Campaign betaCampaign;
    [SerializeField] private BoolVariable betaActive;

    void Awake() => Update();
    
    void Update()
    {
        var activeCampaign = betaActive.Value ? betaCampaign : normalCampaign;
        if (activeCampaign.Name != zone.Campaign.Name)
            zone.Init(activeCampaign);
    }
}
