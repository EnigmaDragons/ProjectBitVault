using System;
using UnityEngine;

public class DestroyIfJumped : OnMessage<PieceMoved>
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Texture deathMask;
    [SerializeField] private float secondsTilDeath = 1;

    private bool _isDying = false;
    private float _t;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.IsAdjacentTo(new TilePoint(gameObject)) && msg.To.IsAdjacentTo(new TilePoint(gameObject)) && (msg.To.X == msg.From.X || msg.To.Y == msg.From.Y))
        {
            Message.Publish(new ObjectDestroyed(gameObject, true));
            StartDying();
        }
    }

    private void StartDying()
    {
        _isDying = true;
        _t = 0;
        renderer.material.SetTexture("_DisplacementMask", deathMask);
        renderer.material.SetFloat("_DefaultShrink", 0);
        renderer.material.SetFloat("_NormalPush", 0);
        renderer.material.SetFloat("_Shrink_Faces_Amplitude", 0);
    }

    private void Update()
    {
        if (!_isDying)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / secondsTilDeath);
        renderer.material.SetFloat("_DefaultShrink", _t);
        renderer.material.SetFloat("_NormalPush", _t);
        if (_t == 1)
            Destroy(gameObject);
    }
}
