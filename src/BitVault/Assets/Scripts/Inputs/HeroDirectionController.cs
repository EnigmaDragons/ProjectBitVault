using System;
using UnityEngine;

public class HeroDirectionController : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap currentLevelMap;
    
    private bool _readyForNewInput = true;

    private void Start()
    {
        currentLevelMap.RegisterAsSelectable(gameObject);
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

            Message.Publish(new MoveByRequested(new TilePoint(1 * Math.Sign(hDir), 0)));
            _readyForNewInput = false;
        }
        else if (Math.Abs(vDir) > 0.01)
        {
            if (!_readyForNewInput) return;
            
            Message.Publish(new MoveByRequested(new TilePoint(0, 1 * Math.Sign(vDir))));
            _readyForNewInput = false;
        }
        else
        {
            _readyForNewInput = true;
        }
    }
}
