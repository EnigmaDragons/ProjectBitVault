using UnityEngine;

public class InGameCollectedStar : MonoBehaviour
{
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private Vector3Reference position1;
    [SerializeField] private Vector3Reference position2;
    [SerializeField] private Vector3Reference position3;
    [SerializeField] private float speed;

    private Vector3Reference _target;

    private void OnEnable()
    {
        if (stars.Count == 0)
            _target = position1;
        else if (stars.Count == 1)
            _target = position2;
        else if (stars.Count == 2)
            _target = position3;
    }

    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _target.Value, speed * Time.deltaTime);
    }
}
