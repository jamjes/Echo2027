using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private InputReader input;

    private void OnEnable()
    {
        if (input == null) return;
            
        input.OnJump += OnJumpReceived;
    }
    
    private void OnDisable()
    {
        if (input == null) return;
        
        input.OnJump -= OnJumpReceived;
    }

    private void Awake()
    {
        if (rb == null) Debug.LogWarning($"{gameObject.name} Jump: Rigidbody2D reference missing");
        if (groundCheck == null) Debug.LogWarning($"{gameObject.name} Jump: GroundCheck reference missing");
        if (input == null) Debug.LogWarning($"{gameObject.name} Jump: InputReader reference missing");
    }

    private void Apply() => rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    
    private void OnJumpReceived()
    {
        if (!groundCheck || !rb) return;
        
        if (rb.bodyType == RigidbodyType2D.Kinematic) return;
        
        var grounded = groundCheck.IsGrounded();
        if (grounded) Apply();
    }
}
