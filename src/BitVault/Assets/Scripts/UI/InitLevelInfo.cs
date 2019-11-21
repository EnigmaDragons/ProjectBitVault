using TMPro;
using UnityEngine;

public class InitLevelInfo : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;
    [SerializeField] private TextMeshProUGUI label;

    private void Awake()
    {
        label.text = $"{level.ActiveLevel.Name}";
    }
}
