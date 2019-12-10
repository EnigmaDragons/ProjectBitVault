using TMPro;
using UnityEngine;

public class InitTutorialInfo : MonoBehaviour
{
    [SerializeField] private CurrentTutorial tutorial;
    [SerializeField] private TextMeshProUGUI label;

    private void Awake()
    {
        label.text = $"{tutorial.Tutorial.DialogueName}";
    }
}
