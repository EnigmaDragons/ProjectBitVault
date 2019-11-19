using UnityEngine;

public class PlayLevelCompleteVictorySound : OnMessage<LevelCompleted>
{
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);
    
    protected override void Execute(LevelCompleted msg) => player.Play(clip, volume);
}