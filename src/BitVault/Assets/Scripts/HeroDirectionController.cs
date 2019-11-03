﻿using System;
using UnityEngine;

public class HeroDirectionController : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap currentLevelMap;
    
    private bool _readyForNewInput = true;

    private void Start()
    {
        Message.Publish(new PieceSelected(gameObject));
    }

    void Update()
    {
        var hDir = Input.GetAxisRaw("Horizontal");
        var vDir = Input.GetAxisRaw("Vertical");

        
        // TODO: Refactor out duplication
        if (Math.Abs(hDir) > 0.01)
        {
            if (!_readyForNewInput) return;

            var newPosition = transform.position + new Vector3(1 * Math.Sign(hDir), 0, 0);
            Message.Publish(new MoveToRequested(new TilePoint(newPosition)));
            _readyForNewInput = false;
        }
        else if (Math.Abs(vDir) > 0.01)
        {
            if (!_readyForNewInput) return;
            
            var newPosition = transform.position + new Vector3(0, 1 * Math.Sign(vDir), 0);
            Message.Publish(new MoveToRequested(new TilePoint(newPosition)));
            _readyForNewInput = false;
        }
        else
        {
            _readyForNewInput = true;
        }
    }
}
