using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "WaveDefinition", menuName = "Scriptable Objects/WaveDefinition")]
public class WaveDefinition : ScriptableObject
{
    public TimelineAsset waveTimeline;
}
