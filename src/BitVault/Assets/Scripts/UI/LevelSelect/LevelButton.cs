using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject locked;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private Navigator navigator;
    [SerializeField] private CurrentLevel currentLevel;
    [SerializeField] private IsLevelUnlockedCondition levelUnlocked;
    [SerializeField] private BoolVariable isLevelStart;
    
    public void Init(int zoneNumber, int levelNum, GameLevel level)
    {
        Init($"{levelNum + 1}", () =>
        {
            currentLevel.SelectLevel(level, zoneNumber, levelNum);
            isLevelStart.Value = true;
            navigator.NavigateToDialogue();
        }, level, levelUnlocked.IsLevelUnlocked(zoneNumber, levelNum));
    }    
    
    public void Init(string text, Action onClick, GameLevel level, bool available)
    {
        textField.text = text;
        button.onClick.AddListener(() => onClick());
        for (var i = 0; i < storage.GetStars(level) && i < stars.Length; i++)
            stars[i].SetActive(true);
        button.interactable = available;
        locked.SetActive(!available);
    }
}
