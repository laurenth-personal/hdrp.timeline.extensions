using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Experimental.Rendering.HDPipeline;

[Serializable]
public class DOFPlayable : PlayableBehaviour
{
    public bool overrideDOFMode = false;
    public DepthOfFieldMode depthOfFieldMode = DepthOfFieldMode.UsePhysicalCamera;
    public bool overrideFocusDistance = false;
    public float focusDistance = 1;
    public bool overrideNearStart = false;
    public float nearStart = 0;
    public bool overrideNearEnd = false;
    public float nearEnd = 4;
    public bool overrideNearMaxRadius = false;
    [Range(0, 8)]
    public float nearMaxRadius = 1;
    public bool overrideFarStart = false;
    public float farStart = 10;
    public bool overrideFarEnd = false;
    public float farEnd = 20;
    public bool overrideFarMaxRadius = false;
    [Range(0,16)]
    public float farMaxRadius = 1;
}

[Serializable]
public class DOFPlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public DOFPlayable dofPlayable = new DOFPlayable();

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<DOFPlayable>.Create(graph, dofPlayable);
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending | ClipCaps.Extrapolation; }
    }
}
