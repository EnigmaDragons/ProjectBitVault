using UnityEngine;
using UnityEngine.UI;

public class ZoneTiledLogo : MonoBehaviour
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
            logo.sprite = zone.LogoTiled;
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, logo.color.a);
        }
    }
}
