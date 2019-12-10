using System.Collections;
using UnityEngine;

public class FallingTile : OnMessage<PieceMoved, UndoPieceMoved>
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material dangerousMaterial;
    [SerializeField] private Material dangerousGoalMaterial;
    [SerializeField] private GameState gameState;
    [SerializeField] private LockBoolVariable gameInputActive;
    [SerializeField] private float _lossDelay;
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private BoolReference tutorialActive;

    private bool _isDangerous = false;
    private Material _originalMaterial;

    private void Awake()
    {
        _originalMaterial = renderer.material;
    }

    private void Revert()
    {
        _isDangerous = false;
        renderer.material = _originalMaterial;
    }
    
    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.Equals(new TilePoint(gameObject)) && !_isDangerous)
        {
            _isDangerous = true;
            if (tutorialActive)
                renderer.material = dangerousMaterial;
            else
                renderer.material = map.BitVaultLocation.IsAdjacentTo(new TilePoint(gameObject)) ? dangerousGoalMaterial : dangerousMaterial;
        }
        else if (msg.To.Equals(new TilePoint(gameObject)) && _isDangerous)
        {
            gameInputActive.Lock(gameObject);
            StartCoroutine(DelayedLoss(tutorialActive.Value));
        }
    }

    protected override void Execute(UndoPieceMoved msg)
    {
        if (_isDangerous && msg.From.Equals(new TilePoint(gameObject)))
            Revert();
    }

    private IEnumerator DelayedLoss(bool isTutorialActive)
    {
        yield return new WaitForSeconds(_lossDelay);
        gameInputActive.Unlock(gameObject);
        if (!isTutorialActive)
            gameState.InitLevel();
    }
}
