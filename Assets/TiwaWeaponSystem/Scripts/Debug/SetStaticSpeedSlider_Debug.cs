
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetStaticSpeedSlider_Debug : UdonSharpBehaviour
{
    public bool isTip;
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Start()
    {
        slider.value = isTip ? velocityEstimator.staticSpeedTip : velocityEstimator.staticSpeedHandle;
    }

    private void Update()
    {
        if (isTip)
        {
            velocityEstimator.staticSpeedTip = slider.value;
            text.text = velocityEstimator.staticSpeedTip.ToString();
        }
        else
        {
            velocityEstimator.staticSpeedHandle = slider.value;
            text.text = velocityEstimator.staticSpeedHandle.ToString();
        }
    }
}
