using TMPro;
using UnityEngine;

public sealed class DisplayVersionNumberText : MonoBehaviour
{
    [SerializeField] private string prefix;
    [SerializeField] private StringReference value;
    [SerializeField] private TextMeshProUGUI text;

    private void Start() => text.text = $"{prefix}{value}";
}
