using System.Linq;
using TMPro;
using UnityEngine;
     
public sealed class InitScreenResolutionOptions : MonoBehaviour
{
    [SerializeField] private DisplaySettings display;
    [SerializeField] private TMP_Dropdown dropdownMenu;
    [SerializeField] private int minWidth = 800;

    private Resolution[] _resolutions;

    void Awake()
    {
        display.SetResolution(Screen.currentResolution);
        _resolutions = Screen.resolutions
            .Where(x => x.width % 16 == 0 && x.height % 9 == 0)
            .Where(x => x.width > minWidth)
            .Distinct()
            .Reverse()
            .ToArray();
        dropdownMenu.onValueChanged.AddListener(SetResolution);
        dropdownMenu.options.Clear();
        var current = display.CurrentResolution;
        for (var i = 0; i < _resolutions.Length; i++)
        {
            var valString = ResToString(_resolutions[i]);
            dropdownMenu.options.Add(new TMP_Dropdown.OptionData(valString));
            if (_resolutions[i].Equals(current))
                dropdownMenu.value = i;
        }

        dropdownMenu.RefreshShownValue();
    }
     
    private string ResToString(Resolution res) => res.width + " x " + res.height;
    private void SetResolution(int index) => display.SetResolution(_resolutions[index]);
}
