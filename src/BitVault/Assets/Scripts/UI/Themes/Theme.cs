using TMPro;
using UnityEngine;

[CreateAssetMenu]
public sealed class Theme : ScriptableObject
{
    [SerializeField] public ColorReference defaultColor;
    [SerializeField] public ColorReference borderTint;
    [SerializeField] public ColorReference dialogueButtonTextTint;
    [SerializeField] public ColorReference menuButtonTextTint;
    [SerializeField] public TMP_ColorGradient menuButtonTextGradient;
    [SerializeField] public ColorReference panelTint;

    public Color ColorFor(ThemeElement element)
    {
        var colors = new DictionaryWithDefault<ThemeElement, Color>(defaultColor)
        {
            { ThemeElement.PrimaryTextColor, menuButtonTextTint },
            { ThemeElement.SecondaryTextColor, dialogueButtonTextTint },
            { ThemeElement.PanelTint, panelTint },
        };
        return colors[element];
    }
}
