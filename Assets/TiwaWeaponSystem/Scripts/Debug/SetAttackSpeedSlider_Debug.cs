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

    public SwingEstimator swingEstimator;

    private void Start()
    {
        slider.value = isTip ? swingEstimator.attackSpeedTip : swingEstimator.attackSpeedHandle;
    }
    private void Update()
    {
        if (isTip)
        {
            swingEstimator.attackSpeedTip = slider.value;
            text.text = swingEstimator.attackSpeedTip.ToString();
        }
        else
        {
            swingEstimator.attackSpeedHandle = slider.value;
            text.text = swingEstimator.attackSpeedHandle.ToString();
        }
    }
}
