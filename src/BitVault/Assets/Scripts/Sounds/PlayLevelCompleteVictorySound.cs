using UnityEngine;

public class PlayLevelCompleteVictorySound : OnMessage<LevelCompleted>
{
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private AudioClip clip;
    
    protected override void Execute(LevelCompleted msg) => player.Play(clip);
}