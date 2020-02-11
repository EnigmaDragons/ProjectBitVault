using System;
using UnityEngine;

[CreateAssetMenu]
public sealed class DisplaySettings : ScriptableObject
{
    [SerializeField] private bool isFullscreen = true;
    [SerializeField] private Resolution resolution;

    private void OnEnable()
    {
        resolution = Screen.currentResolution;
    }

    public bool IsFullscreen => isFullscreen;

    public void SetFullscreen(bool on) => UpdateAfter(() => isFullscreen = on);
    public void ToggleFullscreen() => UpdateAfter(() => isFullscreen = !isFullscreen);
    public Resolution CurrentResolution => resolution;
    public void SetResolution(Resolution r) => UpdateAfter(() => resolution = r);
    
    private void UpdateAfter(Action set)
    {
        var old = $"{isFullscreen}-{resolution.width}x{resolution.height}";
        set();
        var newHash = $"{isFullscreen}-{resolution.width}x{resolution.height}";
        if (newHash != old)
            Screen.SetResolution(resolution.width, resolution.height, isFullscreen);    
    }

}
