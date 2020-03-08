using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCharacterDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 leftPosition;
    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private Vector3 rightPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float secondsToMove;
    [SerializeField] private GameObject staticVfx;

    private float _t;
    private Vector3 _source;
    private Vector3 _destination;
    private bool _leaving;

    public Character Character { get; set; }

    public void Init(Character character)
    {
        Character = character;
        image.sprite = character.Bust;
        staticVfx.SetActive(character.UseStatic);
        transform.localPosition = startPosition;
    }

    public void SetFocus(bool focused) => image.color = focused ? Color.white : Color.gray;

    public void GoTo(DialogueDirection direction, bool isTeleporting)
    {
        _t = isTeleporting ? 1 : 0;
        _source = transform.localPosition;
        if (direction == DialogueDirection.Left)
            _destination = leftPosition;
        else if (direction == DialogueDirection.Center)
            _destination = centerPosition;
        else if (direction == DialogueDirection.Right)
            _destination = rightPosition;
    }

    public void Leave()
    {
        _t = 0;
        _source = transform.localPosition;
        _destination = endPosition;
        _leaving = true;
    }

    private void Update()
    {
        _t = Math.Min(1, _t += Time.deltaTime / secondsToMove);
        transform.localPosition = Vector3.Lerp(_source, _destination, _t);
        if (_leaving && _t == 1)
            Destroy(gameObject);
    }
}
