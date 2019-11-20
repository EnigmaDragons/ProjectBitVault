using E7.Introloop;
using UnityEngine;

[CreateAssetMenu]
public class IntroLoopAudioPlayer : ScriptableObject
{    
    public void PlaySelectedMusicLooping(IntroloopAudio clipToPlay)
    {
        IntroloopPlayer.Instance.Play(clipToPlay);
    }
}
