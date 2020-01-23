using UnityEngine;

namespace Assets.Scripts.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private Campaign zones;
        [SerializeField] private CurrentLevel level;
        [SerializeField] private Navigator navigator;
        [SerializeField] private GameObject button;
        [SerializeField] private SaveStorage storage;
        [SerializeField] private BoolVariable isLevelStart;
        [SerializeField] private CurrentDialogue currentDialogue;
        [SerializeField] private BoolReference AutoSkipStory;

        private void Awake() => button.SetActive(!IsLastLevel || (!IsLastZone && IsNextZoneUnlocked));

        private bool IsLastLevel => zones.Value[level.ZoneNumber].Value.Length == level.LevelNumber + 1;
        private bool IsLastZone => zones.Value.Length == level.ZoneNumber + 1;
        private bool IsNextZoneUnlocked => storage.GetTotalStars() >= zones.Value[level.ZoneNumber + 1].StarsRequired;

        public void Go()
        {
            var nextZone = level.LevelNumber + 1 == zones.Value[level.ZoneNumber].Value.Length ? level.ZoneNumber + 1 : level.ZoneNumber;
            var nextLevel = level.LevelNumber + 1 == zones.Value[level.ZoneNumber].Value.Length ? 0 : level.LevelNumber + 1;
            var gameLevel = zones.Value[nextZone].Value[nextLevel];
            level.SelectLevel(gameLevel, nextZone, nextLevel);
            isLevelStart.Value = true;
            currentDialogue.Set(storage.GetStars(gameLevel) == 0 ? zones.Value[nextZone].CurrentStory() : new Maybe<ConjoinedDialogues>());
            storage.SaveZone(nextZone);
            if (AutoSkipStory.Value || !currentDialogue.Dialogue.IsPresent) 
                navigator.NavigateToGameScene();
            else
                navigator.NavigateToDialogue();
        }
    }
}
