using UnityEngine;

public class MoveTreeAnalysisControl : MonoBehaviour
{
    [SerializeField] private BoolReference _developmentIsActive;
    [SerializeField] private CurrentLevelMap _map;

    private bool _calculating;

    private void Update()
    {
        if (_developmentIsActive.Value && !_calculating && Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.E))
        {
            _calculating = true;
            new MoveTreeAnalysis().CalculateMoveTree(_map.GetSnapshot());
            Debug.Log("CalculatingComplete");
        }
    }
}
