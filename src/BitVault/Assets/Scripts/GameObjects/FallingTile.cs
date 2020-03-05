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
    [SerializeField] private Achievements achievements;

    public bool IsDangerous { get; private set; } = false;
    private Material _originalMaterial;

    private void Awake()
    {
        _originalMaterial = renderer.material;
        IsDangerous = false;
    }

    private void Revert()
    {
        IsDangerous = false;
        renderer.material = _originalMaterial;
    }
    
    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.Equals(new TilePoint(gameObject)) && !IsDangerous)
        {
            IsDangerous = true;
            renderer.material = map.BitVaultLocation.IsAdjacentTo(new TilePoint(gameObject)) ? dangerousGoalMaterial : dangerousMaterial;
        }
        else if (msg.To.Equals(new TilePoint(gameObject)) && IsDangerous)
        {
            map.HasLost = true;
            gameInputActive.Lock(gameObject);
            StartCoroutine(DelayedLoss());
            //I wish i could put this in a seperate object but not sure how
            if (new TilePoint(map.Hero).IsAdjacentTo(map.BitVaultLocation))
                achievements.UnlockAchievement(AchievementType.FailsafeGoal);
        }
    }

    protected override void Execute(UndoPieceMoved msg)
    {
        if (IsDangerous && msg.From.Equals(new TilePoint(gameObject)))
            Revert();
    }

    private IEnumerator DelayedLoss()
    {
        yield return new WaitForSeconds(_lossDelay);
        gameInputActive.Unlock(gameObject);
        Message.Publish(new LevelResetRequested());
    }
}
