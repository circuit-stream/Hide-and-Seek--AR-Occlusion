using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class OcclusionUIManager : MonoBehaviour
{
    public static OcclusionUIManager Instance;

    [SerializeField]
    private TextMeshProUGUI objectSelectionText;

    [SerializeField]
    private Button qualityButton;

    private float deltaTime;

    private TextMeshProUGUI qualityButtonText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        qualityButtonText = qualityButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateQualityText();
    }

    public void UpdateObjectCountText(string newInfo)
    {
        objectSelectionText.text = newInfo;
    }

    public void ToggleQuality()
    {
        EnvironmentDepthMode depthMode = AROcclusionQualityChanger.Instance.GetCurrentDepthMode();

        switch (depthMode)
        {
            case EnvironmentDepthMode.Disabled:
                AROcclusionQualityChanger.Instance.ChangeQualityTo(EnvironmentDepthMode.Fastest);
                break;
            case EnvironmentDepthMode.Fastest:
                AROcclusionQualityChanger.Instance.ChangeQualityTo(EnvironmentDepthMode.Medium);
                break;
            case EnvironmentDepthMode.Medium:
                AROcclusionQualityChanger.Instance.ChangeQualityTo(EnvironmentDepthMode.Best);
                break;
            case EnvironmentDepthMode.Best:
                AROcclusionQualityChanger.Instance.ChangeQualityTo(EnvironmentDepthMode.Disabled);
                break;
        }

        UpdateQualityText();
    }

    private void UpdateQualityText()
    {
        EnvironmentDepthMode newDepthMode = AROcclusionQualityChanger.Instance.GetCurrentDepthMode();
        qualityButtonText.text = $"Quality {newDepthMode}";
    }
    
}
