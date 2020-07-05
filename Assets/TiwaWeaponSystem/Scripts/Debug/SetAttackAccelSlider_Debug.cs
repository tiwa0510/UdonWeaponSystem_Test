
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetAttackAccelSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Start()
    {
        slider.value = velocityEstimator.attackAccel;
    }
    private void Update()
    {
        velocityEstimator.attackAccel= slider.value;
        text.text = velocityEstimator.attackAccel.ToString();
    }
}
