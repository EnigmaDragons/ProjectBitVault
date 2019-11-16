using System;
using UnityEngine;

public class DestroyIfDoubleJumped : OnMessage<PieceMoved>
{
    [SerializeField] private Renderer renderer1;
    [SerializeField] private Renderer renderer2;
    [SerializeField] private Texture deathMask;
    [SerializeField] private float secondsTilDeath = 1;
    [ReadOnly, SerializeField] private int numJumpsRemaining = 2;

    private Renderer _selectedRenderer;
    private bool _isDying = false;
    private float _t;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.IsAdjacentTo(new TilePoint(gameObject)) && msg.To.IsAdjacentTo(new TilePoint(gameObject)) && (msg.To.X == msg.From.X || msg.To.Y == msg.From.Y))
        {
            numJumpsRemaining--;
            if (numJumpsRemaining == 0)
            {
                Message.Publish(new ObjectDestroyed(gameObject, true));
                StartDying(renderer2);
            }
            else
                StartDying(renderer1);
        }
    }

    private void StartDying(Renderer renderer)
    {
        if (numJumpsRemaining == 0)
            _selectedRenderer.gameObject.SetActive(false);
        _selectedRenderer = renderer;
        _isDying = true;
        _t = 0;
        _selectedRenderer.material.SetTexture("_DisplacementMask", deathMask);
        _selectedRenderer.material.SetFloat("_DefaultShrink", 0);
        _selectedRenderer.material.SetFloat("_NormalPush", 0);
        _selectedRenderer.material.SetFloat("_Shrink_Faces_Amplitude", 0);
    }

    private void Update()
    {
        if (!_isDying)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / secondsTilDeath);
        _selectedRenderer.material.SetFloat("_DefaultShrink", _t);
        _selectedRenderer.material.SetFloat("_NormalPush", _t);
        if (_t == 1)
        {
            if (numJumpsRemaining == 0)
                Destroy(gameObject);
            else
                _isDying = false;
        }
    }
}
