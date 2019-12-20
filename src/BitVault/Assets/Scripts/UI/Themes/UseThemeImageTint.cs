using UnityEngine;
using UnityEngine.UI;

public sealed class UseThemeImageTint : MonoBehaviour
{
    [SerializeField] private CurrentTheme theme;
    [SerializeField] private Image image;
    [SerializeField] private ThemeElement element;

    private void Awake() => image.color = theme.ColorFor(element);
}
