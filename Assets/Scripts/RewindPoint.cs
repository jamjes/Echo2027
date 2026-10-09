using UnityEngine;

[System.Serializable]
public struct RewindPoint
{
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; private set; }
    
    public RewindPoint(Vector2 position, Vector2 velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}
