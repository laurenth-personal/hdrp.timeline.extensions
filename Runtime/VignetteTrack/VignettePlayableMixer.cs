using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering;

public class VignettePlayableMixer : PlayableBehaviour
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

        if (!profile.Has<Vignette>())
            return;

        Color newColor = Color.black;
        Vector2 newCenter = Vector2.zero;
        float newIntensity = 0;
        float newSmoothness = 0;
        float newRoundness = 0;
        bool newRounded = false;

    var count = handle.GetInputCount();
        for (var i = 0; i < count; i++)
        {

            var inputHandle = handle.GetInput(i);
            var weight = handle.GetInputWeight(i);

            if (inputHandle.IsValid() &&
                inputHandle.GetPlayState() == PlayState.Playing &&
                weight > 0)
            {
                var data = ((ScriptPlayable<VignettePlayable>)inputHandle).GetBehaviour();
                if (data != null)
                {
                    newColor += data.color * weight;
                    newCenter += data.center * weight;
                    newIntensity += data.intensity * weight;
                    newSmoothness += data.smoothness * weight;
                    newRoundness += data.roundness * weight;
                    newRounded = weight > 0.5 ? data.rounded : newRounded;
                }

            }
        }
        profile.TryGet<Vignette>(out Vignette m_vignette);
        m_vignette.color.value = newColor;
        m_vignette.center.value = newCenter;
        m_vignette.intensity.value = newIntensity;
        m_vignette.smoothness.value = newSmoothness;
        m_vignette.roundness.value = newRoundness;
        m_vignette.rounded.value = newRounded;
    }
}
