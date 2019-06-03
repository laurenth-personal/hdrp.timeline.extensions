using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class DOFPlayableMixer : PlayableBehaviour
{
    // Called each frame the mixer is active, after inputs are processed
    public override void ProcessFrame(Playable handle, FrameData info, object playerData)
    {
        Volume volume = playerData as Volume;

        if (volume == null)
            return;
        DepthOfField m_depthOfField;

        var profile = Application.isPlaying
                ? volume.profile
                : volume.sharedProfile;

        if (!profile.Has<DepthOfField>())
            return;


        var count = handle.GetInputCount();
        //Short loop
        int activeClipsCount = 0;
        for (var i = 0; i < count; i++)
        {

            var inputHandle = handle.GetInput(i);
            var weight = handle.GetInputWeight(i);
            if (weight > 0)
                activeClipsCount += 1;
        }

        if (activeClipsCount == 0)
        {
            volume.weight = 0;
            return;
        }

        volume.weight = 1;

        bool overrideDOFMode = false;
        DepthOfFieldMode depthOfFieldMode = DepthOfFieldMode.Off;
        bool overrideFocusDistance = false;
        float focusDistance = 0;
        bool overrideNearStart = false;
        float nearStart = 0;
        bool overrideNearEnd = false;
        float nearEnd = 0;
        bool overrideFarStart = false;
        float farStart = 0;
        bool overrideFarEnd = false;
        float farEnd = 0;
        bool overrideNearMaxRadius = false;
        float nearMaxRadius = 0;
        bool overrideFarMaxRadius = false;
        float farMaxRadius = 0;

        for (var i = 0; i < count; i++)
        {

            var inputHandle = handle.GetInput(i);
            var weight = handle.GetInputWeight(i);

            if (inputHandle.IsValid() &&
                inputHandle.GetPlayState() == PlayState.Playing &&
                weight > 0)
            {
                var data = ((ScriptPlayable<DOFPlayable>)inputHandle).GetBehaviour();
                if (data != null)
                {
                    if (data.overrideDOFMode)
                    {
                        overrideDOFMode = data.overrideDOFMode;
                        depthOfFieldMode = data.depthOfFieldMode;
                    }
                    if (data.overrideFocusDistance)
                    {
                        overrideFocusDistance = true;
                        focusDistance += data.focusDistance * weight;
                    }
                    if (data.overrideNearStart)
                    {
                        overrideNearStart = true;
                        nearStart += data.nearStart * weight;
                    }
                    if (data.overrideNearEnd)
                    {
                        overrideNearEnd = true;
                        nearEnd += data.nearEnd * weight;
                    }
                    if(data.overrideNearMaxRadius)
                    {
                        overrideNearMaxRadius = true;
                        nearMaxRadius += data.nearMaxRadius * weight;
                    }
                    if (data.overrideFarStart)
                    {
                        overrideFarStart = true;
                        farStart += data.farStart * weight;
                    }
                    if (data.overrideFarEnd)
                    {
                        overrideFarEnd = true;
                        farEnd += data.farEnd * weight;
                    }
                    if (data.overrideFarMaxRadius)
                    {
                        overrideFarMaxRadius = true;
                        farMaxRadius += data.farMaxRadius * weight;
                    }
                }

            }
        }
        profile.TryGet<DepthOfField>(out m_depthOfField);
        m_depthOfField.focusMode.overrideState = overrideDOFMode;
        m_depthOfField.focusMode.value = depthOfFieldMode;
        m_depthOfField.focusDistance.overrideState = overrideFocusDistance;
        m_depthOfField.focusDistance.value = focusDistance;
        m_depthOfField.nearFocusStart.overrideState = overrideNearStart;
        m_depthOfField.nearFocusStart.value = nearStart;
        m_depthOfField.nearFocusEnd.overrideState = overrideNearEnd;
        m_depthOfField.nearFocusEnd.value = focusDistance;
        m_depthOfField.nearMaxBlur.overrideState = overrideNearMaxRadius;
        m_depthOfField.nearMaxBlur.value = nearMaxRadius;
        m_depthOfField.farFocusStart.overrideState = overrideFarStart;
        m_depthOfField.farFocusStart.value = farStart;
        m_depthOfField.farFocusEnd.overrideState = overrideFarEnd;
        m_depthOfField.farFocusEnd.value = farEnd;
        m_depthOfField.farMaxBlur.overrideState = overrideFarMaxRadius;
        m_depthOfField.farMaxBlur.value = farMaxRadius;
    }
}