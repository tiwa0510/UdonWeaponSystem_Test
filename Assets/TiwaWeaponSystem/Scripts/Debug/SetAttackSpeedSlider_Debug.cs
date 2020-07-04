using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetAttackSpeedSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Update()
    {
        velocityEstimator.attackSpeed = slider.value;
        text.text = velocityEstimator.attackSpeed.ToString();
    }
}
