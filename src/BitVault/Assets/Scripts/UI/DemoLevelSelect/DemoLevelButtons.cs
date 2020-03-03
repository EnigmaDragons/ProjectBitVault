using UnityEngine;

public sealed class DemoLevelButtons : MonoBehaviour
{
    [SerializeField] private DemoLevelButton[] buttons;

    public void Init(int zoneNumber, GameLevels zone)
    {
        Debug.Log($"Demo Buttons {zone.Name}");
        for (var i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            var hasLevel = i < zone.Value.Length;
            Debug.Log($"Has Level {i} - {hasLevel}");
            button.gameObject.SetActive(hasLevel);
            if (hasLevel)
                button.Init(zoneNumber, i, zone.Value[i]);
        }
    }
}
