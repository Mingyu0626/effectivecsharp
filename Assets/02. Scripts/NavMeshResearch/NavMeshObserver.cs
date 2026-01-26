using UnityEngine;
using UnityEngine.AI;

public class NavMeshObserver : MonoBehaviour
{
    private void OnEnable()
    {
        NavMesh.onPreUpdate += OnNavMeshUpdated;
    }

    private void OnDisable()
    {
        NavMesh.onPreUpdate -= OnNavMeshUpdated;
    }

    private void OnNavMeshUpdated()
    {
        Debug.Log("NavMesh Update Detected (Carving or Baking)");
    }
}
