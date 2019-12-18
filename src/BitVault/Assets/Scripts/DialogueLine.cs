using System;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField, TextArea] private string text;
    [SerializeField] private Character character;
    [SerializeField] [DTValidator.Optional] private GameObject customDisplay;

    public string Text => text;
    public Character Character => character;
    public Maybe<GameObject> CustomDisplay => customDisplay ? customDisplay : new Maybe<GameObject>();
}
