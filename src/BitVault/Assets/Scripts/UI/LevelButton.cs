using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textField;

    public void Init(string text, Action onClick)
    {
        textField.text = text;
        button.onClick.AddListener(() => onClick());
    }
}
  
