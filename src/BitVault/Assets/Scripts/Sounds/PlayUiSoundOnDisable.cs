using UnityEngine;

public class PlayUiSoundOnDisable : MonoBehaviour
{
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);

    private void OnDisable() => player.Play(clip, volume);
}
