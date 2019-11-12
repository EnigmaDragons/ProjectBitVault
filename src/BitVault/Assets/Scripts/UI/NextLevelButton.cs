using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private GameZones zones;
        [SerializeField] private CurrentLevel level;
        [SerializeField] private Navigator navigator;
        [SerializeField] private GameObject button;

        private void Awake() => button.SetActive(!(zones.Value.Length == level.ZoneNumber + 1 && zones.Value.Last().Value.Length == level.LevelNumber + 1));

        public void Go()
        {
            var nextZone = level.LevelNumber + 1 == zones.Value[level.ZoneNumber].Value.Length ? level.ZoneNumber + 1 : level.ZoneNumber;
            var nextLevel = level.LevelNumber + 1 == zones.Value[level.ZoneNumber].Value.Length ? 0 : level.LevelNumber + 1;
            level.SelectLevel(zones.Value[nextZone].Value[nextLevel], nextZone, nextLevel);
            navigator.NavigateToGameScene();
        }
    }
}
