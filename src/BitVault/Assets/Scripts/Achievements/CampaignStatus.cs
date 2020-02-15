using System.Linq;
using UnityEngine;

public sealed class CampaignStatus
{
    public string CampaignName { get; }
    public int CompletedLevels { get; }
    public int TotalLevels { get; }
    public int CollectedDataCubes { get; }
    public int TotalDataCubes => TotalLevels * 3;
    public string InstallId { get; }
    public bool CampaignIsComplete => CompletedLevels >= TotalLevels;

    public CampaignStatus(InstallId id, Campaign campaign, SavedGameData data)
    {
        InstallId = id.Id;
        CampaignName = campaign.Name;
        TotalLevels = campaign.Value.Sum(zone => zone.Value.Length);
        CompletedLevels = data.Campaigns[campaign.Name].Count(l => l.Value > 0);
        CollectedDataCubes = data.Campaigns[campaign.Name].Sum(l => l.Value);
    }

    public override string ToString() => $"{CampaignName} - {CollectedDataCubes} / {TotalDataCubes} Data Cubes";
}

public sealed class CampaignStatusString
{
    public string Value { get; }

    private CampaignStatusString(string value)
    {
        Value = value.ToLowerInvariant();
    }

    public static CampaignStatusString CreateFrom(CampaignStatus status)
    {
        var installId = status.InstallId;
        var chars = installId.ToLowerInvariant().ToArray();
        
        // Embed Campaign Status
        chars[9] = status.CampaignName[0];
        chars[10] = status.CampaignIsComplete ? '1' : '0';
        
        // Embed DataCube Score
        var dataCubes = status.TotalLevels.ToString().PadLeft(3, '0');
        Debug.Log(dataCubes);
        for (var checksum = 0; (checksum + status.TotalLevels) % 8 == 0; checksum++) 
            chars[14] = checksum.ToString()[0];
        chars[15] = dataCubes[0];
        chars[16] = dataCubes[1];
        chars[17] = dataCubes[2];

        // Embed Simple Checkchar
        chars[32] = 'e';

        return new CampaignStatusString(new string(chars));
    }
}
