using UnityEngine;
using System.Collections.Generic;

public class Rewinder : MonoBehaviour
{
    [SerializeField] private List<RewindPoint> timeline;
    private float elapsedTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 lastVelocity;

    private bool isRewinding = false;
    
    private void Awake()
    {
        timeline = new List<RewindPoint>();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Recall();
            return;
        }
        
        Record();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isRewinding == false)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
            }
            
            isRewinding = true;
        }
        else
        {
            if (isRewinding)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1;
            }
            
            isRewinding = false;
        }
    }

    private void Record()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f / 60)
        {
            if (timeline.Count == 60 * 3)
            {
                timeline.RemoveAt(0);
            }
            
            timeline.Add(new RewindPoint(rb.position, spriteRenderer.sprite, spriteRenderer.flipX));
            elapsedTime = 0f;
        }
    }

    private void Recall()
    {
        if (timeline.Count == 0) return;
        
        elapsedTime -= Time.deltaTime;

        if (elapsedTime <= 0)
        {
            var lastRewindPoint = timeline[^1];

            rb.MovePosition(lastRewindPoint.Position);
            spriteRenderer.sprite = lastRewindPoint.Frame;
            spriteRenderer.flipX = lastRewindPoint.FacingLeft;
            
            timeline.RemoveAt(timeline.Count - 1);
            elapsedTime = 1f / 60;
        }
    }
}
