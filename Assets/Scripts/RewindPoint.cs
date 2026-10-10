using UnityEngine;

[System.Serializable]
public struct RewindPoint
{
    public Vector2 Position { get; private set; }
    public Sprite Frame { get; private set; }
    public bool FacingLeft { get; private set; }
    
    public RewindPoint(Vector2 position, Sprite frame, bool facingLeft)
    {
        Position = position;
        Frame = frame;
        FacingLeft = facingLeft;
    }
}
