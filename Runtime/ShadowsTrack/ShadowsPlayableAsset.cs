using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering;

[Serializable]
public class ShadowsPlayable : PlayableBehaviour
{
    public FloatParameter maxDistance = new FloatParameter(100);
    public IntParameter cascadesCount = new IntParameter(4);
    public ClampedFloatParameter split0 = new ClampedFloatParameter(0.05f,0f,1f);
    public ClampedFloatParameter split1 = new ClampedFloatParameter(0.12f, 0f, 1f);
    public ClampedFloatParameter split2 = new ClampedFloatParameter(0.3f, 0f, 1f);
}

[Serializable]  
public class ShadowsPlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public ShadowsPlayable shadowsPlayable = new ShadowsPlayable();

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<ShadowsPlayable>.Create(graph, shadowsPlayable);
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending | ClipCaps.Extrapolation; }
    }
}
