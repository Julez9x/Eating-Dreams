using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class PostProcessingControl : MonoBehaviour
{

    [SerializeField] private Volume volume;
    void Update()
    {
        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

            if (volume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.contrast.value = Mathf.RoundToInt(localBrightness) * 10 / 5;
            }
        }
    }
}
