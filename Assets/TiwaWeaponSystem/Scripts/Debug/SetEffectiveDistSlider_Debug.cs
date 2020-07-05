
using UdonSharp;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SetEffectiveDistSlider_Debug : UdonSharpBehaviour
{
    public bool isTip;
    public Slider slider;
    public Text text;

    public SwingEstimator swingEstimator;

    private void Start()
    {
        slider.value = isTip ? swingEstimator.effectiveDistTip : swingEstimator.effectiveDistHandle;
    }

    private void Update()
    {
        if (isTip)
        {
            swingEstimator.effectiveDistTip = slider.value;
            text.text = swingEstimator.effectiveDistTip.ToString();
        }
        else
        {
            swingEstimator.effectiveDistHandle = slider.value;
            text.text = swingEstimator.effectiveDistHandle.ToString();
        }
    }
}
