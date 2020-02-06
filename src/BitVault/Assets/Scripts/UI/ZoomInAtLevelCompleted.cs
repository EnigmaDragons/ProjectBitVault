using System;
using UnityEngine;

public class ZoomInAtLevelCompleted : OnMessage<LevelCompleted>
{
    [SerializeField] private float _secondsToZoom;
    [SerializeField] private Vector3 _offset;

    private bool _zooming;
    private Vector3 _startingPosition;
    private float _t;

    protected override void Execute(LevelCompleted msg)
    {
        _zooming = true;
        _startingPosition = Camera.main.gameObject.transform.position;
    } 

    private void Update()
    {
        if (!_zooming)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / _secondsToZoom);
        Camera.main.gameObject.transform.position = Vector3.Lerp(_startingPosition, new Vector3(transform.position.x + _offset.x, transform.position.y + _offset.y, _offset.z), _t);
    }
}
