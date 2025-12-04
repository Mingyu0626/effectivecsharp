using UnityEngine;
using System;

public class DotProductDetector : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target;

    [Header("Detection Settings")]
    [SerializeField] private float _detectionAngle = 90f;

    [Header("Visualization")]
    [SerializeField] private VisualizationSettings _visualSettings;


    [Serializable]
    public struct VisualizationSettings
    {
        public float LineLength;
        public Color ForwardColor;
        public Color ToTargetColor;
        public Color FrontColor;
        public Color BackColor;
    }

    public readonly struct DetectionResult
    {
        public readonly float DotProduct;
        public readonly bool IsInFront;
        public readonly float Angle;

        public DetectionResult(Vector3 forward, Vector3 toTarget)
        {
            DotProduct = Vector3.Dot(forward, toTarget);
            IsInFront = DotProduct > 0;
            Angle = Mathf.Acos(Mathf.Clamp(DotProduct, -1f, 1f)) * Mathf.Rad2Deg;
        }
    }

    private void Awake()
    {
        if (_target == null)
        {
            throw new NullReferenceException("Target의 Transform이 할당되지 않았습니다.");
        }
    }

    private void Update()
    {
        DetectionResult result = CalculateDetection();
        PrintDebugInfo(result);
    }

    private void OnDrawGizmos()
    {
        DetectionResult result = CalculateDetection();
        DrawDebugGizmo(result);
    }

    private DetectionResult CalculateDetection()
    {
        Vector3 forward = transform.forward;
        Vector3 toTarget = (_target.position - transform.position).normalized;
        return new DetectionResult(forward, toTarget);
    }

    private void DrawDebugGizmo(DetectionResult result)
    {
        // 1. 캐릭터 위치에 그려지는 구
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);

        // 2. 목표물 위치에 그려지는 구 (앞/뒤 여부에 따라 색상 변경)
        Gizmos.color = result.IsInFront ? _visualSettings.FrontColor : _visualSettings.BackColor;
        Gizmos.DrawWireSphere(_target.position, 0.3f);

        // 3. 캐릭터와 타겟을 잇는 연결선
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, _target.position);
    }

    void PrintDebugInfo(DetectionResult result)
    {
        string relativeTargetDirection = result.IsInFront ? "앞쪽" : "뒤쪽";
        Debug.Log($"[내적 판별] Dot: {result.DotProduct:F3} | 위치: {relativeTargetDirection} | 각도: {result.Angle:F1}°");
    }
}