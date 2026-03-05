using UnityEngine;

public class TreasureSpawnAnchor : MonoBehaviour
{
    public TreasureController prefab;
    
    [Header("Editor Settings")]
    [SerializeField] private bool _showGizmo = true;
    [SerializeField] private Color _gizmoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        if (!_showGizmo) return;

        // 기즈모 색상 설정
        Gizmos.color = _gizmoColor;

        // 0.5f 반지름의 구체(원) 그리기
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
