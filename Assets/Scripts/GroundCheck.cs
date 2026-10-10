using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    
    public bool IsGrounded()
    {
        var hit = Physics2D.CircleCast(
            origin: feetPos.position, 
            radius: .1f, 
            direction: Vector2.zero,
            distance: 0f, 
            layerMask: groundLayer
        );

        return hit.collider;
    }
}
