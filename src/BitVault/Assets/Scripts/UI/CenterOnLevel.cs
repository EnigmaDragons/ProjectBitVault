﻿using System.Linq;
using UnityEngine;

public class CenterOnLevel : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;

    private void Start()
    {
        var bounds = level.ActiveLevel.Prefab.GetComponentsInChildren<Renderer>().Select(x => x.bounds);
        var boundsCombined = bounds.First();
        bounds.ForEach(x => boundsCombined.Encapsulate(x));
        transform.position = new Vector3(boundsCombined.center.x, boundsCombined.center.y, transform.position.z);
    }
}