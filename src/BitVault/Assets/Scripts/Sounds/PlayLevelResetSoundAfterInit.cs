using UnityEngine;

public class PlayLevelResetSoundAfterInit : OnMessage<LevelReset>
{
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);

    private bool _isSetup;

    protected override void Execute(LevelReset msg)
    {
        if (_isSetup)
            player.Play(clip, volume);
        _isSetup = true;
    }
}