
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetAttackSampleFreamsSlider_Debug : UdonSharpBehaviour
{
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Update()
    {
        velocityEstimator.attackSampleFreams = (int)slider.value;
        text.text = velocityEstimator.attackSampleFreams.ToString();
    }
}
