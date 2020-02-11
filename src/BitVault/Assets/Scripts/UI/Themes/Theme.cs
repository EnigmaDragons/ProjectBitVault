using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public sealed class Theme : ScriptableObject
{
    [SerializeField] public ColorReference defaultColor;
    [SerializeField] public ColorReference borderTint;
    [SerializeField] public ColorReference headerTextColor;
    [SerializeField] public ColorReference dialogueButtonTextTint;
    [SerializeField] public ColorReference menuButtonTextTint;
    [SerializeField] public TMP_ColorGradient menuButtonTextGradient;
    [SerializeField] public ColorReference panelTint;
    [SerializeField] public ColorReference settingsTextTint;
    [SerializeField] public TMP_ColorGradient settingsTextGradient;
    
    public Color ColorFor(ThemeElement element)
    {
        var colors = new DictionaryWithDefault<ThemeElement, Color>(defaultColor)
        {
            { ThemeElement.ButtonTextTint, menuButtonTextTint },
            { ThemeElement.PrimaryTextColor, headerTextColor },
            { ThemeElement.SecondaryTextColor, dialogueButtonTextTint },
            { ThemeElement.PrimaryBorderColor, borderTint },
            { ThemeElement.PanelTint, panelTint },
            { ThemeElement.SettingsTextTint, settingsTextTint },
        };
        return colors[element];
    }

    public TMP_ColorGradient GradientFor(ThemeElement element)
    {
        var gradients = new Dictionary<ThemeElement, TMP_ColorGradient>
        {
            { ThemeElement.ButtonTextGradient, menuButtonTextGradient },
            { ThemeElement.SettingsTextGradient, settingsTextGradient },
        };
        return gradients[element];
    }
}
