using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 12f;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            var grounded = IsGrounded();

            if (grounded)
            {
                Apply();
            }
        }
    }

    private bool IsGrounded()
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

    private void Apply()
    {
        rb.linearVelocityY += jumpForce;
    }
}
