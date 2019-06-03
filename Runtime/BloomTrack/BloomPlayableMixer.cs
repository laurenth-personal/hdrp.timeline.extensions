using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering;

public class BloomPlayableMixer : PlayableBehaviour
{
    // Called each frame the mixer is active, after inputs are processed
    public override void ProcessFrame(Playable handle, FrameData info, object playerData)
    {
        var volume = playerData as Volume;

        if (volume == null)
            return;

        var profile = Application.isPlaying
                ? volume.profile
                : volume.sharedProfile;

        if (!profile.Has<Bloom>())
            return;

        float newIntensity = 0;
        float cumulatedWeight = 0;

        var count = handle.GetInputCount();

        for (var i = 0; i < count; i++)
        {
            var inputHandle = handle.GetInput(i);
            var weight = handle.GetInputWeight(i);

            if (inputHandle.IsValid() &&
                inputHandle.GetPlayState() == PlayState.Playing &&
                weight > 0)
            {
                var data = ((ScriptPlayable<BloomPlayable>)inputHandle).GetBehaviour();
                if (data != null)
                {
                    newIntensity += data.intensity * weight;
                    cumulatedWeight += weight;
                }
            }
        }
        profile.TryGet<Bloom>(out Bloom m_Bloom);
        if (cumulatedWeight == 0)
            m_Bloom.intensity.overrideState = false;
        if(cumulatedWeight > 0)
        {
            m_Bloom.intensity.overrideState = true;
            m_Bloom.intensity.value = newIntensity;
        }
    }
}
