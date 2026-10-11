using TMPro;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    private float time = 0;
    private enum TimeDirection
    {
        Forward = 1,
        Backward = -1
    };
    
    private TimeDirection direction = TimeDirection.Forward;
    
    [SerializeField] private TMP_Text timeUI;
    [SerializeField] private Rewinder rewinder;
    [SerializeField] private Color normalTimeColor;
    [SerializeField] private Color rewindTimeColor;


    private void Awake()
    {
        if (rewinder == null) Debug.LogWarning($"{gameObject.name} WorldTime: Rewinder reference missing");
        if (timeUI == null)  Debug.LogWarning($"{gameObject.name} WorldTime: TimeUI reference missing");
    }

    private void Update()
    {
        if (!timeUI || !rewinder) return;

        if (direction == TimeDirection.Backward && rewinder.RewindPointLinkedList.Count < 1) return;
        
        var isRewinding = rewinder.IsRewinding;
        direction = isRewinding ? TimeDirection.Backward : TimeDirection.Forward;
        timeUI.color = isRewinding ? rewindTimeColor : normalTimeColor;
        
        time += Time.deltaTime * (int)direction;
        timeUI.text = $"{time:0.0}s";
    }
}
