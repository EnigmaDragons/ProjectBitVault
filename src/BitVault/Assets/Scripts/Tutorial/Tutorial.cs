using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private float secondsBeforeReset;
    [SerializeField] private GameObject innerTutorial;
    [SerializeField] private CurrentLevelMap tutorialMap;
    [SerializeField] private CurrentLevelStars tutorialStars;
    [SerializeField] private CurrentSelectedPiece selectedPiece;
    [SerializeField] private BoolVariable tutorialActive;

    private float _secondsLeft = 0;
    private GameObject _innerTutorialInstance;

    private void OnEnable() => tutorialActive.Value = true;
    private void OnDisable() => tutorialActive.Value = false;

    private void Update()
    {
        _secondsLeft -= Time.deltaTime;
        if (_secondsLeft <= 0)
        {
            if (_innerTutorialInstance != null)
                Destroy(_innerTutorialInstance);
            tutorialMap.InitLevel();
            tutorialStars.Reset();
            selectedPiece.Deselect();
            _secondsLeft = secondsBeforeReset;
            _innerTutorialInstance = Instantiate(innerTutorial, transform);
            Message.Publish(new LevelReset());
        }
    }
}
