using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DualSpriteMap : ScriptableObject
{
    public Sprite[] normalFrames;
    public Sprite[] alternativeFrames;
    private Dictionary<Sprite, Sprite> map;

    private void OnEnable()
    {
        if (!IsValid()) return;
        
        map = new Dictionary<Sprite, Sprite>();
        for (var i = 0; i < normalFrames.Length; i++)
        {
            map[normalFrames[i]] = alternativeFrames[i];
        }
    }

    public Sprite GetAlternativeSprite(Sprite normal) => map.GetValueOrDefault(normal);

    public bool IsValid()
    {
        if (normalFrames.Length == 0 || alternativeFrames.Length == 0) return false;
        
        if (normalFrames.Length != alternativeFrames.Length) return false;

        return true;
    }
}
