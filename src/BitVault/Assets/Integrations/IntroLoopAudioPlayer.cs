using E7.Introloop;
using UnityEngine;

[CreateAssetMenu]
public class IntroLoopAudioPlayer : ScriptableObject
{
    private IntroloopAudio _currentClip;
    
    public void PlaySelectedMusicLooping(IntroloopAudio clipToPlay)
    {
        if (_currentClip != null && _currentClip.name == clipToPlay.name) return;
        
        _currentClip = clipToPlay;
        IntroloopPlayer.Instance.Play(clipToPlay);
    }
}
