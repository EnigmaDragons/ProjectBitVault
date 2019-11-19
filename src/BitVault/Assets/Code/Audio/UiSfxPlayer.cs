using UnityEngine;

[CreateAssetMenu]
public sealed class UiSfxPlayer : ScriptableObject
{
    [SerializeField, DTValidator.Optional] private AudioSource source;

    public void Init(AudioSource src) => source = src;
    public void InitIfNeeded(AudioSource src) => source.IfNull(() => Init(src));
    public void Play(AudioClip c, float volume = 1f) => source.PlayOneShot(c, volume);
}
