using UnityEngine;

public sealed class LevelZoneButtons : MonoBehaviour
{
    [SerializeField] private LevelButton[] buttons;

    public void Init(int zoneNumber, GameLevels zone)
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            button.gameObject.SetActive(i < zone.Value.Length);
            button.Init(zoneNumber, i, zone.Value[i]);
        }
    }
}

