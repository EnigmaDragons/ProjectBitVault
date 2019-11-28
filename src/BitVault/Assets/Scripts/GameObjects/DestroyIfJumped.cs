using System;
using UnityEngine;

public class DestroyIfJumped : OnMessage<PieceMoved>
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Texture deathMask;
    [SerializeField] private float secondsTilDeath = 1;

    private bool _isDying = false;
    private float _t;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.HasJumpedOver(gameObject))
        {
            Message.Publish(new ObjectDestroyed(gameObject, true));
            StartDying();
        }
    }

    private void StartDying()
    {
        _isDying = true;
        _t = 0;
        renderers.ForEach(SetupForDeath);
    }

    private void SetupForDeath(Renderer renderer)
    {
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
        renderers.ForEach(ApproachDeath);
        if (_t == 1)
            gameObject.SetActive(false);
    }

    private void ApproachDeath(Renderer renderer)
    {
        renderer.material.SetFloat("_DefaultShrink", _t);
        renderer.material.SetFloat("_NormalPush", _t);
    }
}
