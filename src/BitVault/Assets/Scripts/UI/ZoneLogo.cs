using UnityEngine;
using UnityEngine.UI;

public class ZoneLogo : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private GameZones zones;
    [SerializeField] private Image logo;

    private int _selectedZone = -1;

    private void Update()
    {
        var zoneNum = storage.GetZone();
        if (_selectedZone != zoneNum)
        {
            _selectedZone = zoneNum;
            var zone = zones.Value[_selectedZone];
            logo.sprite = zone.Logo;
            logo.color = new Color(zone.LogoColor.r, zone.LogoColor.g, zone.LogoColor.b, logo.color.a);
        }
    }
}
