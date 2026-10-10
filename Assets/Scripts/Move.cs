using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private InputReader playerInputReader;

    private void Awake()
    {
        if (rb == null) Debug.LogWarning($"{gameObject.name} Move: Rigidbody2D reference missing");
        if (playerInputReader == null) Debug.LogWarning($"{gameObject.name} Move: InputReader reference missing");
    }

    private void FixedUpdate()
    {
        if (!rb || !playerInputReader) return;
        
        if (rb.bodyType == RigidbodyType2D.Kinematic) return;

        var xDirection = playerInputReader.Direction.x;
        
        if (xDirection < 0) rb.linearVelocityX = -speed;
        else if (xDirection > 0) rb.linearVelocityX = speed;
        else rb.linearVelocityX = 0;
    }
}
