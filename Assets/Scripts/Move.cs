using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Update()
    {
        if (rb.bodyType == RigidbodyType2D.Static) return;
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocityX = -speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocityX = speed;
        }
        else
        {
            rb.linearVelocityX = 0;
        }
    }
}
