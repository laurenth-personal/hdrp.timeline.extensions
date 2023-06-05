using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering;

[TrackColor(0, 0, 0)]
[TrackClipType(typeof(ProbeVolumePlayableAsset))]
public class ProbeVolumeTrack : TrackAsset
{
    // override the type of mixer playable used by this track
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var c in GetClips())
        {
            ProbeVolumePlayableAsset probevolumePlayable = (ProbeVolumePlayableAsset)c.asset;
            c.displayName = probevolumePlayable.probeVolumePlayable.scenario ;
        }
        return ScriptPlayable<ProbeVolumePlayableMixer>.Create(graph, inputCount);

    }
}