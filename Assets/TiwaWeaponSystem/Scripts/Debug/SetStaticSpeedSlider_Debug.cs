
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetStaticSpeedSlider_Debug : UdonSharpBehaviour
{
    public bool isTip;
    public Slider slider;
    public Text text;

    public SwingEstimator swingEstimator;

    private void Start()
    {
        slider.value = isTip ? swingEstimator.staticSpeedTip : swingEstimator.staticSpeedHandle;
    }

    private void Update()
    {
        if (isTip)
        {
            swingEstimator.staticSpeedTip = slider.value;
            text.text = swingEstimator.staticSpeedTip.ToString();
        }
        else
        {
            swingEstimator.staticSpeedHandle = slider.value;
            text.text = swingEstimator.staticSpeedHandle.ToString();
        }
    }
}
