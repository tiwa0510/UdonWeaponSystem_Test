
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetEffectiveDistSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Start()
    {
        slider.value = velocityEstimator.effectiveDist;
    }

    private void Update()
    {
        velocityEstimator.effectiveDist = slider.value;
        text.text = velocityEstimator.effectiveDist.ToString();
    }
}
