using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class ResetHistory : MonoBehaviour
{
    // Start is called before the first frame update
    public void Reset()
    {
        Debug.Log("Reset History");
        Camera cam = gameObject.GetComponent<Camera>();
        HDCamera hdCam = HDCamera.Get(cam);
        if(hdCam != null)
        {
            hdCam.volumetricHistoryIsValid = false;
            hdCam.Reset();
        }
    }
}
