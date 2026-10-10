using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radius = .1f;

    private void Awake()
    {
        if (feet == null) Debug.LogWarning($"{gameObject.name} GroundCheck: Feet position reference missing");
    }

    public bool IsGrounded()
    {
        if (!feet) return false;
        
        return Physics2D.OverlapCircle(
            feet.position,
            radius,
            groundLayer
        );
    }
}