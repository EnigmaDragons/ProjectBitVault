using UnityEngine;

namespace Assets.Scripts.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private CurrentZone zone;
        [SerializeField] private CurrentLevel level;
        [SerializeField] private Navigator navigator;
        [SerializeField] private GameObject button;
        [SerializeField] private SaveStorage storage;
        [SerializeField] private BoolVariable isLevelStart;
        [SerializeField] private CurrentDialogue currentDialogue;
        [SerializeField] private BoolReference AutoSkipStory;

        private Campaign _campaign => zone.Campaign;

        private void Awake() => button.SetActive(!IsLastLevel || (!IsLastZone && IsNextZoneUnlocked));

        private bool IsLastLevel => zone.Zone.Value.Length == level.LevelNumber + 1;
        private bool IsLastZone => _campaign.Value.Length == level.ZoneNumber + 1;
        private bool IsNextZoneUnlocked => storage.GetTotalStars() >= _campaign.Value[level.ZoneNumber + 1].StarsRequired;

        public void Go()
        {
            var nextZone = IsLastLevel ? level.ZoneNumber + 1 : level.ZoneNumber;
            var nextLevel = IsLastLevel ? 0 : level.LevelNumber + 1;
            var gameLevel = _campaign.Value[nextZone].Value[nextLevel];
            level.SelectLevel(gameLevel, nextZone, nextLevel);
            isLevelStart.Value = true;
            currentDialogue.Set(storage.GetStars(gameLevel) == 0 ? _campaign.Value[nextZone].CurrentStory() : new Maybe<ConjoinedDialogues>());
            storage.SaveZone(nextZone);
            if (AutoSkipStory.Value || !currentDialogue.Dialogue.IsPresent) 
                navigator.NavigateToGameScene();
            else
                navigator.NavigateToDialogue();
        }
    }
}
