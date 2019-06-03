using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering;

[TrackColor(0.5f, 0, 1)]
[TrackClipType(typeof(VignettePlayableAsset))]
[TrackBindingType(typeof(Volume))]
public class VignetteTrack : TrackAsset
{
    // override the type of mixer playable used by this track
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var c in GetClips())
        {
            VignettePlayableAsset postprocessingPlayable = (VignettePlayableAsset)c.asset;
            c.displayName = postprocessingPlayable.vignettePlayable.intensity.ToString();
        }
        return ScriptPlayable<VignettePlayableMixer>.Create(graph, inputCount);

    }
}

