using System;
using TMPro;
using UnityEngine;

public class DailyHintBonus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintBonus;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private int dailyBonusAmount;
    [SerializeField] private float messageTransitionTime;

    private bool _givingDailyBonus;

    private void Start()
    {
        _givingDailyBonus = !storage.HasDailyBonusBeenGiven();
        hintBonus.text = $"Daily Bonus +{dailyBonusAmount}";
        hintBonus.color = new Color(hintBonus.color.r, hintBonus.color.g, hintBonus.color.b, 0);
    }

    private void Update()
    {
        if (_givingDailyBonus)
        {
            hintBonus.color = new Color(hintBonus.color.r, hintBonus.color.g, hintBonus.color.b, Math.Min(1, hintBonus.color.a + Time.deltaTime / messageTransitionTime));
            if (hintBonus.color.a == 1)
            {
                storage.GiveDailyBonus();
                storage.SetHintPoints(storage.GetHintPoints() + 4);
                _givingDailyBonus = false;
            }
        }
        else
            hintBonus.color = new Color(hintBonus.color.r, hintBonus.color.g, hintBonus.color.b, Math.Max(0, hintBonus.color.a - Time.deltaTime / messageTransitionTime));
    }
}
