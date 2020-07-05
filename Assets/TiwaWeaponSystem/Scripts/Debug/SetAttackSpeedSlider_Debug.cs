using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetAttackSpeedSlider_Debug : UdonSharpBehaviour
{
    public bool isTip;
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Start()
    {
        slider.value = isTip ? velocityEstimator.attackSpeedTip : velocityEstimator.attackSpeedHandle;
    }
    private void Update()
    {
        if (isTip)
        {
            velocityEstimator.attackSpeedTip = slider.value;
            text.text = velocityEstimator.attackSpeedTip.ToString();
        }
        else
        {
            velocityEstimator.attackSpeedHandle = slider.value;
            text.text = velocityEstimator.attackSpeedHandle.ToString();
        }
    }
}
