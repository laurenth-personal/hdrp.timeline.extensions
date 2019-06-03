using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class BloomPlayable : PlayableBehaviour
{
    [Range(0, 1)]
    public float intensity;
}

[Serializable]
public class BloomPlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public BloomPlayable BloomPlayable = new BloomPlayable();

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<BloomPlayable>.Create(graph, BloomPlayable);
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending | ClipCaps.Extrapolation; }
    }
}
