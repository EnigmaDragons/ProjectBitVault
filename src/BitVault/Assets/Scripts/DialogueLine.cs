using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField] private string text;
    [SerializeField] private Sprite character;

    public string Text => text;
    public Sprite Character => character;
}
