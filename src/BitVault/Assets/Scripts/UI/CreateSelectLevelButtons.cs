using UnityEngine;

public class CreateSelectLevelButtons : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private GameState state;
    [SerializeField] private GameLevels levels;
    [SerializeField] private LevelButton levelButton;
    [SerializeField] private GameObject buttonPanel;

    private void Awake()
    {
        for (var i = 0; i < levels.Value.Length; i++)
        {
            var level = levels.Value[i];
            var button = Instantiate(levelButton, buttonPanel.transform);
            button.Init($"Level {i + 1}", () =>
            {
                state.SelectLevel(level);
                navigator.NavigateToGameScene();
            });
        }
    }
}
  
