using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class VignettePlayable : PlayableBehaviour
{
    public VignetteMode mode;
    public Color color;
    public Vector2 center;
    [Range(0, 1)]
    public float intensity;
    [Range(0, 1)]
    public float smoothness;
    [Range(0, 1)]
    public float roundness;
    public bool rounded;
}

[Serializable]
public class VignettePlayableAsset : PlayableAsset, ITimelineClipAsset
{
    public VignettePlayable vignettePlayable = new VignettePlayable();

    // Create the runtime version of the clip, by creating a copy of the template
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<VignettePlayable>.Create(graph, vignettePlayable);
    }

    // Use this to tell the Timeline Editor what features this clip supports
    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending | ClipCaps.Extrapolation; }
    }
}
