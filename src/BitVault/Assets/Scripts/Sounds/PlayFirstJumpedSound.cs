using UnityEngine;

public class PlayFirstJumpedSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);

    private bool _isFirstJump;
    
    private void OnEnable()
    {
        _isFirstJump = true;
        Message.Subscribe<PieceMoved>(Execute, this);
    }

    private void OnDisable() => Message.Unsubscribe(this);
    
    private void Execute(PieceMoved msg)
    {
        if (!_isFirstJump || !msg.HasJumpedOver(gameObject)) return;
        
        source.PlayOneShot(clip, volume);
        _isFirstJump = false;
    }
}
