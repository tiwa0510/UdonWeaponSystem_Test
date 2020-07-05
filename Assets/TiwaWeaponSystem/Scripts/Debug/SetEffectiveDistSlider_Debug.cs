
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetEffectiveDistSlider_Debug : UdonSharpBehaviour
{
    public bool isTip;
    public Slider slider;
    public Text text;

    public VelocityEstimator velocityEstimator;

    private void Start()
    {
        slider.value = isTip ? velocityEstimator.effectiveDistTip : velocityEstimator.effectiveDistHandle;
    }

    private void Update()
    {
        if (isTip)
        {
            velocityEstimator.effectiveDistTip = slider.value;
            text.text = velocityEstimator.effectiveDistTip.ToString();
        }
        else
        {
            velocityEstimator.effectiveDistHandle = slider.value;
            text.text = velocityEstimator.effectiveDistHandle.ToString();
        }
    }
}
