using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockNewZoneRequirements : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CurrentZone currentZone;
    [SerializeField] private GameObject locked;
    [SerializeField] private Button nextZoneButton;
    [SerializeField] private BoolVariable developmentToolsEnabled;

    private Campaign campaign => currentZone.Campaign;
    private int _zone = 0;

    private void OnEnable()
    {
        UpdateRequirements();
    }

    private void Update()
    {
        if (_zone != storage.GetZone())
            UpdateRequirements();
    }

    private void UpdateRequirements()
    {
        _zone = storage.GetZone();

        if (campaign.Value.Length == _zone + 1 || developmentToolsEnabled.Value)
        {
            if (text != null)
                text.text = "";
            locked.SetActive(false);
            nextZoneButton.interactable = true;
        }
        else if (storage.GetLevelsCompletedInZone(campaign.Value[_zone]) < campaign.Value[_zone].Value.Length)
        {
            if (text != null)
                text.text = $"{campaign.Value[_zone].Value.Length - storage.GetLevelsCompletedInZone(campaign.Value[_zone])} Levels";
            locked.SetActive(true);
            nextZoneButton.interactable = false;
        }
        else if (storage.GetTotalStars() < campaign.Value[_zone + 1].StarsRequired)
        {
            if (text != null)
                text.text = $"{campaign.Value[_zone + 1].StarsRequired} Data Cubes";
            locked.SetActive(true);
            nextZoneButton.interactable = false;
        }
        else
        {
            if (text != null)
                text.text = "";
            locked.SetActive(false);
            nextZoneButton.interactable = true;
        }
    }
}
