using TMPro;
using UnityEngine;

public class HintPointsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintPoints;
    [SerializeField] private SaveStorage storage;

    private void Update() => hintPoints.text = $"{storage.GetHintPoints()}";
}
