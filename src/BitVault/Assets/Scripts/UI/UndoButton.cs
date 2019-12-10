using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private MoveHistory history;

    private void OnEnable() => history.OnChanged.Subscribe(UpdateButton, this);
    private void OnDisable() => history.OnChanged.Unsubscribe(this);
    private void Awake() => UpdateButton();

    private void UpdateButton()
    {
        if (history.Count < 1)
        {
            text.text = "";
            image.enabled = false;
        }
        else
        {
            text.text = history.Count.ToString();
            image.enabled = true;
        }
    }
}
