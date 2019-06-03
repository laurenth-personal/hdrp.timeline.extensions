using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering;

[TrackColor(0.5f, 0, 1)]
[TrackClipType(typeof(BloomPlayableAsset))]
[TrackBindingType(typeof(Volume))]
public class BloomTrack : TrackAsset
{
    // override the type of mixer playable used by this track
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var c in GetClips())
        {
            BloomPlayableAsset postprocessingPlayable = (BloomPlayableAsset)c.asset;
            c.displayName = postprocessingPlayable.BloomPlayable.intensity.ToString();
        }
        return ScriptPlayable<BloomPlayableMixer>.Create(graph, inputCount);
    }
}

