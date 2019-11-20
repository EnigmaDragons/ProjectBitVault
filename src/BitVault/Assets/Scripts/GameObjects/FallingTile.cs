using System.Collections;
using UnityEngine;

public class FallingTile : OnMessage<PieceMoved>
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material dangerousMaterial;
    [SerializeField] private Material dangerousGoalMaterial;
    [SerializeField] private GameState gameState;
    [SerializeField] private LockBoolVariable gameInputActive;
    [SerializeField] private float _lossDelay;
    [SerializeField] private CurrentLevelMap map;

    private bool _isDangerous = false;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.Equals(new TilePoint(gameObject)) && !_isDangerous)
        {
            _isDangerous = true;
            renderer.material = map.BitVaultLocation.IsAdjacentTo(new TilePoint(gameObject)) ? dangerousGoalMaterial : dangerousMaterial;
        }
        else if (msg.To.Equals(new TilePoint(gameObject)) && _isDangerous)
        {
            gameInputActive.Lock(gameObject);
            StartCoroutine(DelayedLoss());
        }
    }

    private IEnumerator DelayedLoss()
    {
        yield return new WaitForSeconds(_lossDelay);
        gameInputActive.Unlock(gameObject);
        gameState.InitLevel();
    }
}
