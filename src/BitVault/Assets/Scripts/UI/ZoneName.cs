using TMPro;
using UnityEngine;

public class ZoneName : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private GameZones zones;
    [SerializeField] private TextMeshProUGUI text;

    private int _selectedZone = -1;

    private void Update()
    {
        var zone = storage.GetZone();
        if (_selectedZone != zone)
        {
            _selectedZone = zone;
            text.text = zones.Value[_selectedZone].Name;
        }
    }
}