using UnityEngine;

public class JumpingTutorial : MonoBehaviour
{
    [SerializeField] private GameObject mouse;
    [SerializeField] private GameObject jumper;
    [SerializeField] private GameObject selected;
    [SerializeField] private GameObject jumped;
    [SerializeField] private Vector3 jumpToPosition;
    [SerializeField] private Vector3 mouseStartPosition;
    [SerializeField] private float secondsPerTransition;
    [SerializeField] private float mouseSpeed;

    private Vector3 _jumperStartPosition;

    private float _secondsTilNextTransition;
    private int _step = 0;
    private Vector3 _mouseTarget;

    private void Start()
    {
        _secondsTilNextTransition = secondsPerTransition;
        _step = 0;
        _mouseTarget = jumper.transform.localPosition;
        _jumperStartPosition = jumper.transform.localPosition;
    } 

    private void Update()
    {
        mouse.transform.localPosition = Vector3.MoveTowards(mouse.transform.localPosition, _mouseTarget, mouseSpeed * Time.deltaTime);
        _secondsTilNextTransition -= Time.deltaTime;
        if (_secondsTilNextTransition <= 0)
        {
            _secondsTilNextTransition += secondsPerTransition;
            _step++;
            if (_step == 6)
                _step = 0;

            if (_step == 0)
            {
                selected.gameObject.SetActive(false);
                jumper.transform.localPosition = _jumperStartPosition;
                jumped.gameObject.SetActive(true);
                _mouseTarget = jumper.transform.localPosition;
            }
            else if (_step == 1)
            {
                selected.gameObject.SetActive(true);
            }
            else if (_step == 2)
            {
                _mouseTarget = jumpToPosition;
            }
            else if (_step == 3)
            {
                jumper.transform.localPosition = jumpToPosition;
                jumped.gameObject.SetActive(false);
            }
            else if (_step == 4)
            {
                _mouseTarget = mouseStartPosition;
            }
        }

    }
}
