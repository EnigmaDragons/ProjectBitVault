using UnityEngine;

public class PlayUiSoundOnEnable : MonoBehaviour
{
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);

    private void OnEnable() => player.Play(clip, volume);
}