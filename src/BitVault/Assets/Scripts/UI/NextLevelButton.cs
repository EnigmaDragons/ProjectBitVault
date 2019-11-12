using UnityEngine;

namespace Assets.Scripts.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private GameLevels levels;
        [SerializeField] private CurrentLevel level;
        [SerializeField] private Navigator navigator;
        [SerializeField] private GameObject button;

        private void Awake() => button.SetActive(levels.Value.Length != level.NextLevel);

        public void Go()
        {
            level.SelectLevel(levels.Value[level.NextLevel], level.NextLevel);
            navigator.NavigateToGameScene();
        }
    }
}
