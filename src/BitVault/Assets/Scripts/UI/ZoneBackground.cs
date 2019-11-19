using UnityEngine;
using UnityEngine.UI;

public class ZoneBackground : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private GameZones zones;
    [SerializeField] private Image background;

    private int _selectedZone = -1;

    private void Update()
    {
        var zoneNum = storage.GetZone();
        if (_selectedZone != zoneNum)
        {
            _selectedZone = zoneNum;
            var color = zones.Value[_selectedZone].BackgroundColor;
            background.color = new Color(color.r, color.g, color.b, background.color.a);
        }
    }
}
