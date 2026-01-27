using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class AutoMoveObstacle : MonoBehaviour
{
    [SerializeField]
    private float _additionalDistance;
    [SerializeField]
    private float _movementTime;

    private NavMeshObstacle _navMeshObstacle;
    private float _moveThreshold;
    private float _timeToStationary;

    private void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _moveThreshold = _navMeshObstacle.carvingMoveThreshold;
        _timeToStationary = _navMeshObstacle.carvingTimeToStationary;
    }

    private void Start()
    {
        transform.DOMoveY(_moveThreshold + _additionalDistance, _movementTime)
             .SetRelative();
    }
}
