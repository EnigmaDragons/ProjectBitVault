using UnityEngine;
using UnityEngine.UI;

public sealed class UseThemeImageTint : MonoBehaviour
{
    [SerializeField] private CurrentTheme theme;
    [SerializeField] private Image image;
    [SerializeField] private ThemeElement element;
    [SerializeField] private bool fullyOpaque;

    private void Awake()
    {
        image.color = theme.ColorFor(element);
        if (fullyOpaque)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    } 
}
