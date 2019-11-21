using UnityEngine;

public class TutorialCollectedStar : MonoBehaviour
{
    [SerializeField] private Vector3Reference goToLocation;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, goToLocation.Value, speed * Time.deltaTime);
    }
}
