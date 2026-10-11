using UnityEngine;

public struct RewindPoint
{
    public readonly Vector2 Position;
    public readonly Sprite Frame;
    public readonly bool FacingLeft;
    
    public RewindPoint(Vector2 position, Sprite frame, bool facingLeft)
    {
        Position = position;
        Frame = frame;
        FacingLeft = facingLeft;
    }

    public bool IsEqual(RewindPoint target)
    {
        return
            Position == target.Position &&
            Frame == target.Frame &&
            FacingLeft == target.FacingLeft;
    }
}
