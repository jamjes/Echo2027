using UnityEngine;

public struct RewindPoint
{
    public readonly Vector2 Position;
    public readonly Sprite NormalFrame;
    public readonly Sprite AlternativeFrame;
    public readonly bool FacingLeft;
    
    public RewindPoint(Vector2 position, Sprite normalFrame, Sprite alternativeFrame, bool facingLeft)
    {
        Position = position;
        NormalFrame = normalFrame;
        AlternativeFrame = alternativeFrame;
        FacingLeft = facingLeft;
    }

    public bool IsEqual(RewindPoint target)
    {
        return
            Position == target.Position &&
            NormalFrame == target.NormalFrame &&
            AlternativeFrame == target.AlternativeFrame &&
            FacingLeft == target.FacingLeft;
    }
}
