using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering;

[Serializable]
public class ProbeVolumePlayable : PlayableBehaviour
{
    public FloatParameter blendValue = new FloatParameter(0);
    public string scenario = "";
}

[Serializable]  
public class ProbeVolumePlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public ProbeVolumePlayable probeVolumePlayable = new ProbeVolumePlayable();

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<ProbeVolumePlayable>.Create(graph, probeVolumePlayable);
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }
}
