using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField] private string text;
    [SerializeField] [DTValidator.Optional] private GameObject canvasDisplay;
    [SerializeField] [DTValidator.Optional] private GameObject nonCanvasDisplay;

    public string Text => text;
    public Maybe<GameObject> CanvasDisplay => canvasDisplay ? canvasDisplay : new Maybe<GameObject>(); 
    public Maybe<GameObject> NonCanvasDisplay => nonCanvasDisplay ? nonCanvasDisplay : new Maybe<GameObject>();
}
