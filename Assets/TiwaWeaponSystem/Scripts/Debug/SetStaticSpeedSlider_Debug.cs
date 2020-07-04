
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetStaticSpeedSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Update()
    {
        velocityEstimator.staticSpeed = slider.value;
        text.text = velocityEstimator.staticSpeed.ToString();
    }
}
