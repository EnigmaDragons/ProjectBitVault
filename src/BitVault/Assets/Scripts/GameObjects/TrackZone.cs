using TMPro;
using UnityEngine;

public class TrackZone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI zoneNumberText;
    [SerializeField] private SaveStorage saveStorage;

    public void UpdateZone()
    {
        saveStorage.SaveZone(int.Parse(zoneNumberText.text) - 1);
    }
}
