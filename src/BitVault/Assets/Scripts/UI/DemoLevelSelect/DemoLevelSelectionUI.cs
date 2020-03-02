using UnityEngine;
using System;

public sealed class DemoLevelSelectionUI : MonoBehaviour
{
    [SerializeField] private DemoLevelButtons buttons;
    [SerializeField] private CurrentZone zone;
    [SerializeField] private TutorialButton tutorialButton;

    private Campaign Campaign => zone.Campaign;
    private int _zoneIndex;

    public void Start()
    {
        _zoneIndex = Math.Min(Math.Max(0, 0), Campaign.Value.Length - 1);
        zone.Init(_zoneIndex);
        Render();
    }

    private void Render()
    {
        buttons.Init(_zoneIndex, Campaign.Value[_zoneIndex]);
        tutorialButton.Init(_zoneIndex, Campaign);
    }
}
