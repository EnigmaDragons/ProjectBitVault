using UnityEngine;
using UnityEngine.UI;

public sealed class PlayButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    [SerializeField] private UiSfxPlayer player;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);
    
    private void Awake() => GetComponent<Button>().onClick.AddListener(() => player.Play(sound, volume));
}
