using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioSource normalSource;
    [SerializeField] private AudioSource reverseSource;
    [SerializeField] private InputReader inputReader;
    
    [SerializeField] private AudioClip reverseClip;
    [SerializeField] private AudioClip normalClip;
    
    private void OnEnable()
    {
        if (inputReader == null) return;

        inputReader.OnRewindStart += ApplyReverse;
        inputReader.OnRewindEnd += ApplyNormal;
    }

    private void OnDisable()
    {
        if (inputReader == null) return;

        inputReader.OnRewindStart -= ApplyReverse;
        inputReader.OnRewindEnd -= ApplyNormal;
    }

    private void Awake()
    {
        if (inputReader == null) Debug.LogWarning($"{gameObject.name} Level Music: InputReader reference missing");
        if (normalSource == null) Debug.LogWarning($"{gameObject.name} Level Music: NormalSource missing");
        if (reverseSource == null) Debug.LogWarning($"{gameObject.name} Level Music: ReverseSource missing");
        if (normalClip == null) Debug.LogWarning($"{gameObject.name} Level Music: NormalClip missing");
        else normalSource.clip = normalClip;
        if (reverseClip == null)  Debug.LogWarning($"{gameObject.name} Level Music: ReverseClip missing");
        else reverseSource.clip = reverseClip;
    }

    private void Start()
    {
        if (IsMissingReference()) return;
        
        reverseSource.Pause();
        normalSource.Play();
    }

    private void ApplyNormal()
    {
        if (IsMissingReference()) return;
        
        SwitchSource(reverseSource, normalSource);
    }

    private void ApplyReverse()
    {
        if (IsMissingReference()) return;
        
        SwitchSource(normalSource, reverseSource);
    }

    private bool IsMissingReference()
    {
        if (reverseSource == null) return true;
        if (reverseClip == null) return true;
        if (normalSource == null) return true;
        if (normalClip == null) return true;

        return false;
    }

    private void SwitchSource(AudioSource current, AudioSource target)
    {
        current.Pause();
        var currentTime = current.time;
        var startTime = target.clip.length - currentTime;
        target.time = startTime;
        target.UnPause();
    }
}
