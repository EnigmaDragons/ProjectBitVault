using UnityEngine;
using UnityEngine.Audio;

public sealed class InitAudioVolumeLevel : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string valueName = "MusicVolume";
    
    private void Awake()
    {
        var volume = PlayerPrefs.GetFloat(valueName, 0.5f);
        mixer.SetFloat(valueName, Mathf.Log10(volume) * 20);
    }
}
