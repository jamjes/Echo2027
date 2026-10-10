using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int Run = Animator.StringToHash("run_light");
    private static readonly int Idle = Animator.StringToHash("idle_light");
    private static readonly int Jump = Animator.StringToHash("jump_light");
    private static readonly int Fall = Animator.StringToHash("fall_light");
    
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private InputReader input;

    private void OnEnable()
    {
        if (input == null) return;
        
        input.OnRewindStart += StopAnimations;
        input.OnRewindEnd += ResumeAnimations;
    }

    private void OnDisable()
    {
        if (input == null) return;
        
        input.OnRewindStart -= StopAnimations;
        input.OnRewindEnd -= ResumeAnimations;
    }

    private void Awake()
    {
        if (rb == null) Debug.LogWarning($"{gameObject.name} AnimationHandler: Rigidbody2D reference missing");
        if (groundCheck == null) Debug.LogWarning($"{gameObject.name} AnimationHandler: GroundCheck reference missing");
        if (spriteRenderer == null) Debug.LogWarning($"{gameObject.name} AnimationHandler: SpriteRenderer reference missing");
        if (animator == null) Debug.LogWarning($"{gameObject.name} AnimationHandler: Animator reference missing");
        if (input == null) Debug.LogWarning($"{gameObject.name} AnimationHandler: InputReader reference missing");
    }

    void Update()
    {
        if (!animator) return;

        if (!input) return;
        
        var isGrounded = groundCheck.IsGrounded();
        
        if (!isGrounded)
        {
            if (rb.linearVelocityY > 0) animator.CrossFade(Jump, 0, 0);
            else if (rb.linearVelocityY < 0) animator.CrossFade(Fall, 0, 0);
        }
        else
        {
            var xDirection = input.Direction.x;

            if (Mathf.Abs(xDirection) > 0.01f)
            {
                if (xDirection < 0 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
                else if (xDirection > 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
                
                animator.CrossFade(Run, 0, 0);
            }
            else animator.CrossFade(Idle, 0, 0);
        }
    }

    private void StopAnimations() => animator.enabled = false;
    
    private void ResumeAnimations() => animator.enabled = true;
}
