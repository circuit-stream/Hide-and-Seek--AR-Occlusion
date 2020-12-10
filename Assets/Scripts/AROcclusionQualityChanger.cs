using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AROcclusionQualityChanger : MonoBehaviour
{
    public static AROcclusionQualityChanger Instance;

    private AROcclusionManager arOcclusionManager;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
        arOcclusionManager = GetComponent<AROcclusionManager>();    
    }

    public void ChangeQualityTo(EnvironmentDepthMode environmentDepthMode)
    {
        arOcclusionManager.requestedEnvironmentDepthMode = environmentDepthMode;
    }

    public EnvironmentDepthMode GetCurrentDepthMode()
    {
        return arOcclusionManager.requestedEnvironmentDepthMode;
    }
}
