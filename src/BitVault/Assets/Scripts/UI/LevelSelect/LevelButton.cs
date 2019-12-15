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
    [SerializeField] private CurrentDialogue currentDialogue;
    [SerializeField] private GameZones zones;
    
    public void Init(int zoneNumber, int levelNum, GameLevel level)
    {
        Init($"{levelNum + 1}", () =>
        {
            currentLevel.SelectLevel(level, zoneNumber, levelNum);
            isLevelStart.Value = true;
            currentDialogue.Set(zones.Value[zoneNumber].CurrentStory());
            navigator.NavigateToDialogue();
        }, level, levelUnlocked.IsLevelUnlocked(zoneNumber, levelNum));
    }

    private void Init(string text, Action onClick, GameLevel level, bool available)
    {
        gameObject.SetActive(true);
        textField.text = text;
        button.onClick.AddListener(() => onClick());
        for (var i = 0; i < stars.Length; i++)
            stars[i].SetActive(storage.GetStars(level) > i);
        button.interactable = available;
        locked.SetActive(!available);
    }
}
