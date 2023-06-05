using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class ProbeVolumePlayableMixer : PlayableBehaviour
{
    ProbeReferenceVolume instance;

    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);

        instance = ProbeReferenceVolume.instance;
        if (instance == null)
            Debug.Log("Probe volume instance not found");
    }

    // Called each frame the mixer is active, after inputs are processed
    public override void ProcessFrame(Playable handle, FrameData info, object playerData)
    {
        if(instance != null)
        {
            var count = handle.GetInputCount();
            for (var i = 0; i < count; i++)
            {
                var inputHandle = handle.GetInput(i);
                var weight = handle.GetInputWeight(i);

                if (inputHandle.IsValid() &&
                    inputHandle.GetPlayState() == PlayState.Playing &&
                    weight > 0)
                {
                    var data = ((ScriptPlayable<ProbeVolumePlayable>)inputHandle).GetBehaviour();
                    if (data != null)
                    {
                        if (weight >= 1.0 && instance.lightingScenario != data.scenario)
                        {
                            instance.lightingScenario = data.scenario;
                        }
                        if (data.scenario != instance.lightingScenario)
                            instance.BlendLightingScenario(data.scenario, weight);
                    }

                }
            }
        }

    }
}
