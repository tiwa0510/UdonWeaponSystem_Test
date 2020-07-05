using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetVelocitySampleSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public SwingEstimator swingEstimator;

    void Start()
    {
        slider.value = swingEstimator.velocitySampleFreams;
    }

    private void Update()
    {
        swingEstimator.velocitySampleFreams = (int)slider.value;
        text.text = swingEstimator.velocitySampleFreams.ToString();
    }
}
