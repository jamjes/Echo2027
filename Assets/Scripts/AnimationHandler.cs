using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private int runLight = Animator.StringToHash("run_light");
    private int idleLight = Animator.StringToHash("idle_light");
    private int jumpLight = Animator.StringToHash("jump_light");
    private int fallLight = Animator.StringToHash("fall_light");
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (animator.isActiveAndEnabled)
            {
                animator.enabled = false;
            }
            
            return;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (!animator.isActiveAndEnabled)
            {
                animator.enabled = true;
            }
        }
        
        var isGrounded = groundCheck.IsGrounded();
        
        if (!isGrounded)
        {
            if (rb.linearVelocityY > 0)
            {
                animator.CrossFade(jumpLight, 0, 0);
            }
            else if (rb.linearVelocityY < 0)
            {
                animator.CrossFade(fallLight, 0, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A) && !spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = true;
                }
                else if (Input.GetKey(KeyCode.D) && spriteRenderer.flipX)
                {
                    spriteRenderer.flipX = false;
                }
                
                animator.CrossFade(runLight, 0, 0);
            }

            if (rb.linearVelocityX == 0)
            {
                animator.CrossFade(idleLight, 0, 0);
            }
        }
    }
}
