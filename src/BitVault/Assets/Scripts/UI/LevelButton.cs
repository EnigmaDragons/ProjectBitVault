using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private Sprite star;
    [SerializeField] private Image[] stars;

    public void Init(string text, Action onClick, GameObject level)
    {
        textField.text = text;
        button.onClick.AddListener(() => onClick());
        ShowStars(level);
    }

    private void ShowStars(GameObject level)
    {
        var key = StringValues.StarsForLevel(level);
        if (PlayerPrefs.HasKey(key))
            for (var i = 0; i < PlayerPrefs.GetInt(key) && i < stars.Length; i++)
                stars[i].sprite = star;
    }
}
  
