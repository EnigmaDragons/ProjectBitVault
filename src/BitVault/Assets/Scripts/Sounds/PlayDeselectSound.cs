using UnityEngine;

public class PlayDeselectSound : OnMessage<PieceDeselected>
{
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);
    
    protected override void Execute(PieceDeselected msg) => player.Play(clip, volume);
}