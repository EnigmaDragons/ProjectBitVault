using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public sealed class OnVideoFinished : MonoBehaviour
{
    [SerializeField] private UnityEvent onFinished;

    private void Awake() => GetComponent<VideoPlayer>().loopPointReached += _ => onFinished.Invoke();
}
  
