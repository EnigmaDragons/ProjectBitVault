using TMPro;
using UnityEngine;

public class InitLevelInfo : MonoBehaviour
{
    [SerializeField] private CurrentZone zone;
    [SerializeField] private BoolReference isLevelStart;
    [SerializeField] private TextMeshProUGUI label;

    private void Awake()
    {
        var name = isLevelStart.Value
            ? zone.Zone.CurrentStory().Intro.DialogueName
            : zone.Zone.CurrentStory().Outro.DialogueName;
        label.text = $"{name}";
    }
}
