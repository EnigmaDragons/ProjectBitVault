using UnityEngine;
using UnityEngine.UI;

public class SelectHackerCharacter : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private Button male;
    [SerializeField] private Button female;
    [SerializeField] private BoolVariable useFemale;

    private void Awake()
    {
        male.onClick.AddListener(SelectMale);
        female.onClick.AddListener(SelectFemale);
    }

    private void SelectMale()
    {
        useFemale.Value = false;
        navigator.NavigateToLevelSelect();
    }

    private void SelectFemale()
    {
        useFemale.Value = true;
        navigator.NavigateToLevelSelect();
    }
}
