using UnityEngine;
using UnityEngine.UI;

public class StarTrackerTutorial : MonoBehaviour
{
    [SerializeField] private Sprite empty;
    [SerializeField] private Sprite filled;
    [SerializeField] private float secondsPerTransition;
    [SerializeField] private Image[] images;

    private float _secondsRemaining;
    private float _starsFilled = 0;

    private void Start() => _secondsRemaining = secondsPerTransition;

    private void Update()
    {
        _secondsRemaining -= Time.deltaTime;
        if (_secondsRemaining <= 0)
        {
            _secondsRemaining += secondsPerTransition;
            _starsFilled++;
            if (_starsFilled > images.Length)
                _starsFilled = 0;
            for (var i = 0; i < images.Length; i++)
                images[i].sprite = _starsFilled > i ? filled : empty;
        }
    }
}
