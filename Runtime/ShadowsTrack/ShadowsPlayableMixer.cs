using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ShadowsPlayableMixer : PlayableBehaviour
{
    // Called each frame the mixer is active, after inputs are processed
    public override void ProcessFrame(Playable handle, FrameData info, object playerData)
    {
        if (playerData == null)
            return;
        var volume = playerData as Volume;

        HDShadowSettings m_shadowSettings;

        var profile = Application.isPlaying
                ? volume.profile
                : volume.sharedProfile;

        if (!profile.Has<HDShadowSettings>())
            return;

        NoInterpMinFloatParameter newMaxDistance = new NoInterpMinFloatParameter(0,0,false);
        NoInterpClampedIntParameter newCascadeCount = new NoInterpClampedIntParameter(1,1,4,false);
        FloatParameter newSplit0 = new FloatParameter(0);
        FloatParameter newSplit1 = new FloatParameter(0);
        FloatParameter newSplit2 = new FloatParameter(0);

        var count = handle.GetInputCount();
        for (var i = 0; i < count; i++)
        {

            var inputHandle = handle.GetInput(i);
            var weight = handle.GetInputWeight(i);

            if (inputHandle.IsValid() &&
                inputHandle.GetPlayState() == PlayState.Playing &&
                weight > 0)
            {
                var data = ((ScriptPlayable<ShadowsPlayable>)inputHandle).GetBehaviour();
                if (data != null)
                {
                    if(data.maxDistance.overrideState)
                    {
                        newMaxDistance.overrideState = true;
                        newMaxDistance.value = data.maxDistance.value* weight;
                    }
                    if(data.cascadesCount.overrideState)
                    {
                        newCascadeCount.overrideState = true;
                        newCascadeCount.value = Mathf.FloorToInt((float)data.cascadesCount.value * weight);
                    }
                    if(data.split0.overrideState)
                    {
                        newSplit0.overrideState = true;
                        newSplit0.value = data.split0.value * weight;
                    }
                    if (data.split1.overrideState)
                    {
                        newSplit1.overrideState = true;
                        newSplit1.value = data.split1.value * weight;
                    }
                    if (data.split2.overrideState)
                    {
                        newSplit2.overrideState = true;
                        newSplit2.value = data.split2.value * weight;
                    }
                }

            }
        }
        profile.TryGet<HDShadowSettings>(out m_shadowSettings);

        m_shadowSettings.maxShadowDistance.overrideState = newMaxDistance.overrideState;
        m_shadowSettings.maxShadowDistance.value = newMaxDistance.value;
        m_shadowSettings.cascadeShadowSplitCount.overrideState = newCascadeCount.overrideState;
        m_shadowSettings.cascadeShadowSplitCount.value = newCascadeCount.value;
        m_shadowSettings.cascadeShadowSplit2.overrideState = newSplit2.overrideState;
        m_shadowSettings.cascadeShadowSplit1.overrideState = newSplit1.overrideState;
        m_shadowSettings.cascadeShadowSplit0.overrideState = newSplit0.overrideState;
        m_shadowSettings.cascadeShadowSplit2.value = newSplit2.value;
        m_shadowSettings.cascadeShadowSplit1.value = newSplit1.value;
        m_shadowSettings.cascadeShadowSplit0.value = newSplit0.value;
    }
}
