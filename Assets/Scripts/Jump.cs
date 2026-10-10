using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private GroundCheck groundCheck;
    
    
    private void Update()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic) return;
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            var grounded = groundCheck.IsGrounded();

            if (grounded)
            {
                Apply();
            }
        }
    }

    private void Apply()
    {
        rb.linearVelocityY += jumpForce;
    }
}
