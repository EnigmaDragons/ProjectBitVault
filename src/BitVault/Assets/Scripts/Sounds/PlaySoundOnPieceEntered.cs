﻿using UnityEngine;

public class PlaySoundOnPieceEntered : OnMessage<PieceMoved>
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private FloatReference volume = new FloatReference(0.5f);
    
    protected override void Execute(PieceMoved msg)
    {
        if (msg.To.Equals(new TilePoint(gameObject)))
            source.PlayOneShot(clip, volume);
    }
}