using TMPro;
using UnityEngine;

public class StarObjectiveText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] starObjectiveTexts;
    [SerializeField] private CurrentLevel currentLevel;

    private void Awake()
    {
        var starObjectives = currentLevel.ActiveLevel.StarObjectives;
        for (var i = 0; i < starObjectiveTexts.Length && i < starObjectives.Length; i++)
            starObjectiveTexts[i].text = starObjectives[i].Objective;
    }
}
