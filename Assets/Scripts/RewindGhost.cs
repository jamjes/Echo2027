using UnityEngine;

public class RewindGhost : MonoBehaviour
{
    [SerializeField] private Rewinder rewinder;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private RewindPoint furthestRewindPoint;

    private void Awake()
    {
        if (rewinder == null) Debug.LogWarning($"{gameObject.name} RewindGhost: Rewinder reference missing");
        if (spriteRenderer == null) Debug.LogWarning($"{gameObject.name} RewindGhost: SpriteRenderer reference missing");
    }

    private void FixedUpdate()
    {
        if (!rewinder || !spriteRenderer) return;
        
        if (rewinder.RewindPointLinkedList == null || rewinder.RewindPointLinkedList.Count < 1) return;
        
        var rewindPoint = rewinder.RewindPointLinkedList.First.Value;

        if (furthestRewindPoint.IsEqual(rewindPoint)) return;
        
        transform.position = rewindPoint.Position;
        spriteRenderer.sprite = rewindPoint.NormalFrame;
        spriteRenderer.flipX = rewindPoint.FacingLeft;
        
        furthestRewindPoint = rewindPoint;
    }
}