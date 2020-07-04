using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetVelocitySampleSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    void Start()
    {
    }

    private void Update()
    {
        velocityEstimator.velocitySampleFreams = (int)slider.value;
        text.text = velocityEstimator.velocitySampleFreams.ToString();
    }
}
