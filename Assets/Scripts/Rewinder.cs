using UnityEngine;
using System.Collections.Generic;

public class Rewinder : MonoBehaviour
{
    private LinkedList<RewindPoint> rewindPointLinkedList;
    private float elapsedTime;
    [SerializeField] private int frameRate = 60;
    [SerializeField] private float rewindLength = 3f;
    private float recordInterval;
    private int rewindPointMaxLength;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isRewinding = false;
    [SerializeField] private InputReader input;

    private void OnEnable()
    {
        if (input == null) return;
        
        input.OnRewindStart += BeginRecall;
        input.OnRewindEnd += EndRecall;
    }

    private void OnDisable()
    {
        if (input == null) return;
        
        input.OnRewindStart -= BeginRecall;
        input.OnRewindEnd -= EndRecall;
    }

    private void Awake()
    {
        rewindPointLinkedList = new LinkedList<RewindPoint>();
        recordInterval = 1f / frameRate;
        rewindPointMaxLength = (int)(rewindLength * frameRate);
        
        if (rb == null) Debug.LogWarning($"{gameObject.name} Rewinder: Rigidbody2D reference missing");
        if (spriteRenderer == null) Debug.LogWarning($"{gameObject.name} Rewinder: SpriteRenderer reference missing");
    }

    private void FixedUpdate()
    {
        if (!rb || !spriteRenderer) return;
        
        if (isRewinding) Recall();
        else Record();
    }

    private void Record()
    {
        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime >= recordInterval)
        {
            if (rewindPointLinkedList.Count == rewindPointMaxLength)
            {
                rewindPointLinkedList.RemoveFirst();
            }
            
            rewindPointLinkedList.AddLast(new RewindPoint(rb.position, spriteRenderer.sprite, spriteRenderer.flipX));
            elapsedTime = 0f;
        }
    }

    private void Recall()
    {
        if (rewindPointLinkedList.Count == 0) return;
        
        elapsedTime -= Time.fixedDeltaTime;

        if (elapsedTime <= 0)
        {
            var lastRewindPoint = rewindPointLinkedList.Last.Value;

            rb.position = lastRewindPoint.Position;
            
            spriteRenderer.sprite = lastRewindPoint.Frame;
            spriteRenderer.flipX = lastRewindPoint.FacingLeft;
            
            rewindPointLinkedList.RemoveLast();
            
            elapsedTime = recordInterval;
        }
    }

    private void BeginRecall()
    {
        if (!rb) return;
        
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        isRewinding = true;
    }

    private void EndRecall()
    {
        if (!rb) return;
        
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        isRewinding = false;
    }
}